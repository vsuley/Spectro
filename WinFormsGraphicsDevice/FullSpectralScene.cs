using System;
using System.Windows.Forms;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SpectroNamespace
{
    class FullSpectralScene : CommonScene
    {
        public override void LoadContent()
        {
            // Load base content
            base.LoadContent();

            // The base class must have loaded a generic effect. Replace with our own effect.
            try
            {
                _effect = _contentManager.Load<Effect>("Effect1");
            }
            catch (Exception e)
            {
                MessageBox.Show("Loading Effect1 failed with message:" + e.Message);
                throw;
            }
        }

        /// <summary>
        /// Some added setup logic to prepare the models/shaders for final rendering.
        /// </summary>
        public override void PrepareForRendering()
        {
            base.PrepareForRendering();

            try
            {
                // Setup Technique
                _effect.CurrentTechnique = _effect.Techniques["TechniqueWithoutTexture"];

                // Phong shading
                _effect.Parameters["LightPosition"].SetValue(new Vector4(0f, 50f, 7000f, 1f));

                // Ambient and Diffuse
                _effect.Parameters["AmbientReflectance"].SetValue(1.0f);
                _effect.Parameters["DiffuseReflectance"].SetValue(1.0f);

                // Specular component
                _effect.Parameters["SpecularReflectance"].SetValue(0.7f);
                _effect.Parameters["SpecularColor"].SetValue(new Vector4(1f, 1f, 1f, 1f));
                _effect.Parameters["Shininess"].SetValue(3.0f);
                _effect.Parameters["CameraPosition"].SetValue(_cameraPosition);

                // Other constants
                _effect.Parameters["HDR"].SetValue(false);
            }
            catch (Exception e)
            {
                MessageBox.Show("FullSpectral scene failed to initialize render state becaues: " + e.Message);
                throw;
            }
        }
    }
}
