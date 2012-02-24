using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System.Windows.Forms.DataVisualization.Charting;

namespace SpectroNamespace
{
    public partial class Spectro : Form
    {
        private DataManager _dataManager;
        private RenderSettings _renderSettings;
        private string _selectedObjectName;
        private int _selectedObjectId;

        public Spectro()
        {
            InitializeComponent();
            _dataManager = new DataManager();
            _renderSettings = new RenderSettings();
        }

        private void Spectro_Load(object sender, EventArgs e)
        {
            // Initialize the data manager. This will read in all data files and prepare internal data model.
            _dataManager.InitializeData();

            // Initilialize the utilities calss with datamanager 
            Utilities.TheDataManager = _dataManager;

            // Initialize the two main rendering controls now that all other data has been initialized. SEQUENCE OF OPERATIONS IS IMPORTANT.
            CommonScene[] scenes = new CommonScene[] { fullSpectralScene1, rgbScene1 };
            foreach (CommonScene scene in scenes)
            {
                scene.RenderSettings = _renderSettings;
                scene.DataManager = _dataManager;
                scene.LoadContent();
                scene.PrepareForRendering();
            }

            // Object selection state.
            _selectedObjectId = -1;
            _selectedObjectName = string.Empty;

            // Set Light Source in the scene
            lightSourceComboBox.Items.AddRange(_dataManager.LightSources.ToArray());
            lightSourceComboBox.SelectedIndex = 0;

            // Set MaterialComboBox starting state.
            materialComboBox.Items.AddRange(_dataManager.Materials.ToArray());
            materialComboBox.SelectedIndex = 0;
            materialComboBox.Enabled = false;

            // Set Observers.
            observerComboBox.Items.AddRange(_dataManager.Observers.ToArray());
            observerComboBox.SelectedIndex = 0;

            // Set key value.
            int startingKeyValue = 1;
            KeyValueBar.Value = startingKeyValue;
            KeyValueCounter.Text = startingKeyValue.ToString();
            _renderSettings.Key = startingKeyValue;

            // Set Light Strength
            int startingLightStrength = 60;
            LightStrengthBar.Value = startingLightStrength;
            LightStrengthCounter.Text = startingLightStrength.ToString();
            _renderSettings.LightStrength = startingLightStrength;

            // Set ClipInvisible checkbox.
            clipInvisbleCheckBox.CheckState = CheckState.Checked;

            // Set Charts displays
            UpdateMaterialChart(_dataManager.Materials[0]);
            UpdateLightChart(_dataManager.LightSources[0]);
            UpdateObserverChart(_dataManager.Observers[0]);

            // Update some other things.
            fullSpectralScene1.Animate = animateCheckbox.Checked;
            rgbScene1.Animate = animateCheckbox.Checked;
        }

        # region UI Change Event Handlers.

        /// <summary>
        /// Event handler for when the Key value bar is scrolled.
        /// </summary>
        private void KeyValueBar_Scroll(object sender, EventArgs e)
        {
            this.KeyValueCounter.Text = KeyValueBar.Value.ToString();
            _renderSettings.Key = KeyValueBar.Value;

            RefreshScene();
        }

        /// <summary>
        /// Event handler for when the light strength is changed.
        /// </summary>
        private void LightStrengthBar_Scroll(object sender, EventArgs e)
        {
            this.LightStrengthCounter.Text = LightStrengthBar.Value.ToString();
            _renderSettings.LightStrength = LightStrengthBar.Value;
            RefreshScene();
        }

        private void observerComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            _renderSettings.ActiveObserverIndex = observerComboBox.SelectedIndex;
            
            UpdateObserverChart((Observer)observerComboBox.SelectedItem);
            RefreshScene();
        }

        private void lightSourceComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            _renderSettings.ActiveLightSourceIndex = lightSourceComboBox.SelectedIndex;

            UpdateLightChart((LightSource)lightSourceComboBox.SelectedItem);
            RefreshScene();
        }

        private void materialComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_selectedObjectId > -1)
            {
                fullSpectralScene1.UpdateSelectedObjectMaterial(_selectedObjectName, (Material)materialComboBox.SelectedItem);
                rgbScene1.UpdateSelectedObjectMaterial(_selectedObjectName, (Material)materialComboBox.SelectedItem);
                UpdateMaterialChart((Material)materialComboBox.SelectedItem);
            }

            RefreshScene();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (clipInvisbleCheckBox.CheckState == CheckState.Checked)
            {
                _renderSettings.ClipInvisible = true;
            }
            else if(clipInvisbleCheckBox.CheckState == CheckState.Unchecked)
            {
                _renderSettings.ClipInvisible = false;
            }

            RefreshScene();
        }

        private void animateCheckbox_CheckedChanged(object sender, EventArgs e)
        {
            fullSpectralScene1.Animate = animateCheckbox.Checked;
            rgbScene1.Animate = animateCheckbox.Checked;
        }

        #endregion

        /// <summary>
        /// Marks the scene as changed and invalidates UI that needs to be redrawn.
        /// </summary>
        private void RefreshScene()
        {
            // Flag objects for resync and invalidate UI elements.
            fullSpectralScene1.Recompute = true;
            rgbScene1.Recompute = true;
        }

        private void fullSpectralScene1_Click(object sender, EventArgs e)
        {
            ModelNode modelNode =  fullSpectralScene1.GetIntersectingModel();

            if (modelNode != null)
            {
                this._selectedObjectId = modelNode.NodeId;
                this._selectedObjectName = modelNode.Name;
                this.objectSelectedMessage.Text = modelNode.Name + " is selected.";
                
                this.materialComboBox.Enabled = true;
                this.materialComboBox.SelectedItem = modelNode.Material;
            }
            else
            {
                this._selectedObjectId = -1;
                this._selectedObjectName = String.Empty;
                this.objectSelectedMessage.Text = "Nothing selected";
                this.materialComboBox.Enabled = false;
            }
        }

        private void UpdateMaterialChart(Material material)
        {
            SpectralData data = material.ReflectanceDistribution;
            Series series = MaterialChart.Series[0];

            series.Points.Clear();
            for (int wavelength = data.LowestWavelength, i = 0; wavelength <= data.HighestWavelength; wavelength += data.StepSize, i++)
            {
                series.Points.Add(new DataPoint((float)wavelength, data.WaveData[i]));
            }
        }

        private void UpdateLightChart(LightSource light)
        {
            SpectralData data = light.SpectralPowerDistribution;
            Series series = LightSourceChart.Series[0];

            series.Points.Clear();
            for (int wavelength = data.LowestWavelength, i = 0; wavelength <= data.HighestWavelength; wavelength += data.StepSize, i++)
            {
                series.Points.Add(new DataPoint((float)wavelength, data.WaveData[i]));
            }
        }

        private void UpdateObserverChart(Observer observer)
        {
            int channel = 0;
            foreach (SpectralData spectrum in observer.ResponseSpectra)
            {
                Series series = ObserverChart.Series[channel];

                series.Points.Clear();
                for (int wavelength = spectrum.LowestWavelength, i = 0; wavelength <= spectrum.HighestWavelength; wavelength += spectrum.StepSize, i++)
                {
                    series.Points.Add(new DataPoint((float)wavelength, spectrum.WaveData[i]));
                }

                channel++;
            }
        }
    }
}
