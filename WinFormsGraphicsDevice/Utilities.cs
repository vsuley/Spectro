using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace SpectroNamespace
{
    static class Utilities
    {
        public static DataManager TheDataManager {get; set; }

        public static Matrix XYZtoRGBMatrix = new Matrix(0.030747121f, -0.011160431f, 0.000768247f, 0f,
                                                            -0.0152383f, 0.02059972f, -0.00227443f, 0f,
                                                            -0.003660673f, 7.59E-06f, 0.010599771f, 0f,
                                                            0f, 0f, 0f, 1f);

        public static Vector3 ToVector3(Vector4 vec4)
        {
            return new Vector3(vec4.X, vec4.Y, vec4.Z);
        }

        public static Vector4 GetEquivalentRGB(Material material)
        {
            Vector3 materialXYZ = Utilities.CalculateTristimulusValues(
                TheDataManager.GetLightSourceByPartialName("D65"),
                material,
                TheDataManager.GetObserverByPartialName("1964"),
                true);
            
            return Utilities.CalculateRGBfromXYZ(materialXYZ);
        }

        public static Vector4 GetEquivalentRGB(LightSource lightSource)
        {
            Vector3 lightXYZ = Utilities.CalculateTristimulusValues(
                lightSource,
                TheDataManager.GetMaterialByPartialName("White"),
                TheDataManager.GetObserverByPartialName("1964"),
                true);
            return Utilities.CalculateRGBfromXYZ(lightXYZ);
        }

        public static Vector4 CalculateRGBfromXYZ(Vector3 XYZ)
        {
            Vector4 colorVector = new Vector4();
            Vector4.Transform(ref XYZ, ref XYZtoRGBMatrix, out colorVector);

            return colorVector;
        }

        /// <summary>
        /// Calculate the tristimulus values for the given combination of light source, material and observer.
        /// </summary>
        /// <returns>A float array of size 3 containing X, Y and Z in each cell respectively.</returns>
        public static Vector3 CalculateTristimulusValues(LightSource lightSource, Material material, Observer observer, bool clipInvisible = false)
        { 
            // TODO: This may not be the most efficient of all approaches, make sure you change this to be more streamlined.

            // Normalize spectra.
            //
            List<SpectralData> spectraBank = new List<SpectralData>();
            spectraBank.Add(lightSource.SpectralPowerDistribution);
            spectraBank.Add(material.ReflectanceDistribution);
            spectraBank.Add(observer.ResponseSpectra[0]);
            spectraBank.Add(observer.ResponseSpectra[1]);
            spectraBank.Add(observer.ResponseSpectra[2]);
            
            Utilities.NormalizeSpectra(spectraBank);

            // Calculate the normalizing constant for tristimulus integration
            //
            float K = Utilities.TristimulusNormalizingConstant(lightSource, observer);

            float summation;
            int stepSize = lightSource.SpectralPowerDistribution.StepSize;
            
            // The wave data we need is nested deep inside objects. So grab it into local arrays
            // for convenience of coding.
            //
            float[] lightSourceData, materialData, observerXData, observerYData, observerZData;

            if (clipInvisible)
            {
                // Find out what indexes correspond to wavelengths between 380 and 780 nm.
                // Since the data is normalized, calculating based on 1 source should be enough.
                int startIndex = (380 - lightSource.SpectralPowerDistribution.LowestWavelength) / lightSource.SpectralPowerDistribution.StepSize;
                int count = (780 - 380) / lightSource.SpectralPowerDistribution.StepSize + 1;

                // Sanity check
                if (startIndex < 0)
                {
                    throw new ArgumentException("wavelength data provided started after 380 nm");
                }

                lightSourceData = lightSource.SpectralPowerDistribution.WaveData.GetRange(startIndex, count).ToArray();
                materialData = material.ReflectanceDistribution.WaveData.GetRange(startIndex, count).ToArray();
                observerXData = observer.ResponseSpectra[0].WaveData.GetRange(startIndex, count).ToArray();
                observerYData = observer.ResponseSpectra[1].WaveData.GetRange(startIndex, count).ToArray();
                observerZData = observer.ResponseSpectra[2].WaveData.GetRange(startIndex, count).ToArray();
            }
            else
            {
                lightSourceData = lightSource.SpectralPowerDistribution.WaveData.ToArray();
                materialData = material.ReflectanceDistribution.WaveData.ToArray();
                observerXData = observer.ResponseSpectra[0].WaveData.ToArray();
                observerYData = observer.ResponseSpectra[1].WaveData.ToArray();
                observerZData = observer.ResponseSpectra[2].WaveData.ToArray();
            }
    
            // Calculate the L*M product array. This reduces the repeated multiplication operations that would otherwise be 
            // required.
            //
            float[] lmProductArray = Utilities.ComputeLMProductArray(lightSourceData, materialData);

            Vector3 tristimulusValues = new Vector3();

            // Calculate X
            //
            summation = Utilities.ComputeSummationTerm(lmProductArray, observerXData);
            tristimulusValues.X = K * summation * (float)stepSize;
    
            // Calculate Y
            //
            summation = Utilities.ComputeSummationTerm(lmProductArray, observerYData);
            tristimulusValues.Y = K * summation * (float)stepSize;
    
            // Calculate Z
            //
            summation = Utilities.ComputeSummationTerm(lmProductArray, observerZData);
            tristimulusValues.Z = K * summation * (float)stepSize;

            return tristimulusValues;
        }

        /// <summary>
        /// Calculate the tristimulus values from the data manager object.
        /// </summary>
        /// <param name="dataManager">The datamanager object that contains all the light sources, materials, observers</param>
        /// <returns>A Vector3 containing X, Y and Z values in each cell respectively.</returns>
        public static Vector3 CalculateTristimulusValues(DataManager dataManager)
        {
            LightSource ls = dataManager.LightSources[0];
            Material mat = dataManager.Materials[0];
            Observer obs = dataManager.Observers[0];

            return CalculateTristimulusValues(ls, mat, obs);
        }
        
        /// <summary>
        /// Calculates the Tristimulus normalizing constant using the formula 
        /// K = 100.0 / (total * stepSize) 
        /// Where total is the sum of products of lightsource power and observers Y channel response
        /// </summary>
        /// <param name="lightSource">Light source</param>
        /// <param name="observer">The observer</param>
        /// <returns>The calculated tristimulus constant</returns>
        public static float TristimulusNormalizingConstant(LightSource lightSource, Observer observer)
        {
            int stepSize = lightSource.SpectralPowerDistribution.StepSize;
            int start = lightSource.SpectralPowerDistribution.LowestWavelength;
            int end = lightSource.SpectralPowerDistribution.HighestWavelength;
    
            int i, index;
    
            float[] observerYData = observer.ResponseSpectra[1].WaveData.ToArray();
            float[] lightSourceData = lightSource.SpectralPowerDistribution.WaveData.ToArray();

            float total = (float)0.0;
            for (i=start, index=0; i <= end; i += stepSize, index++)
            {
                total += lightSourceData[index] * observerYData[index];
            }
    
            return (float)100.0 / (total * (float)stepSize);
        }

        /// <summary>
        /// Computes the LM product array for the given light spectrum and material reflectance spectrum.
        /// </summary>
        /// <param name="lightData">Spectral data for light source as a float array</param>
        /// <param name="materialData">Response spectra for material as a float array</param>
        /// <returns>A float array representing the LM product array</returns>
        public static float[] ComputeLMProductArray(float[] lightData, float[] materialData)
        {
            List<float> lmProductArray = new List<float>();
    
            int index = 0;
            for (index=0; index < lightData.Length; index++)
            {
                float temp =  lightData[index] * materialData[index];

                lmProductArray.Add(temp);
            }
    
            return lmProductArray.ToArray();
        }

        /// <summary>
        /// Calculates the sum of products of lmData and observerData 
        /// </summary>
        /// <param name="lmData">lmData array as float array</param>
        /// <param name="observerData">observer data as float array.</param>
        /// <returns>Summed term as a float</returns>
        public static float ComputeSummationTerm(float[] lmData, float[] observerData)
        {
            float summation = (float)0.0;
    
            int index = 0;
            for (index=0; index < lmData.Length; index++)
            {
                summation +=  lmData[index] * observerData[index];        
            }
    
            return summation;
        }

        /// <summary>
        /// This method makes sure that all spectra passed in have the same step size, if not then an error is thrown.
        /// It also equalizes the lowest and highest wavelengths.
        /// </summary>
        /// <param name="spectraBank">All the spectra that must be compared and equalized.</param>
        public static void NormalizeSpectra(List<SpectralData> spectraBank)
        {
            int index;
            int stepSize;
            int lowestWavelength;
            int highestWavelength;
    
            // Get specra count
            //
            int spectraCount = spectraBank.Count;
    
            // Make sure step size is consistent. And while looping, also find lowest and highest wavelengths
            //
            stepSize = spectraBank[0].StepSize;
            lowestWavelength = spectraBank[0].LowestWavelength;
            highestWavelength = spectraBank[0].HighestWavelength;
    
            for(index = 1; index < spectraCount; index++)
            {
                if(stepSize != spectraBank[index].StepSize)
                {
                    throw new InvalidOperationException("Step sizes amongst the chosen spectra were not consistent. This error cannot be recovered from");
                }
        
                int candidateForLow = spectraBank[index].LowestWavelength;
                int candidateForHigh = spectraBank[index].HighestWavelength;
        
                if(candidateForLow < lowestWavelength)
                {
                    lowestWavelength = candidateForLow;
                }
        
                if(candidateForHigh > highestWavelength)
                {
                    highestWavelength = candidateForHigh;
                }
            }
    
            // Normalize the lowest & highest wavelengths for all spectra
            //
            for(index = 0; index < spectraCount; index++)
            {
                SpectralData temp = spectraBank[index]; 
        
                if(temp.LowestWavelength != lowestWavelength)
                {
                    temp.StretchStartWavelength(lowestWavelength);
                }
        
                if(temp.HighestWavelength != highestWavelength)
                {
                    temp.StretchEndWavelength(highestWavelength);
                }
            }
        }
    }
}
