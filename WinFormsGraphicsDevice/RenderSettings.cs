using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace SpectroNamespace
{
    class RenderSettings
    {
        // Properties relating to the overall scene
        public bool PerPixelLighting { get; set; }
        public bool ClipInvisible { get; set; }
        public int ActiveLightSourceIndex { get; set; }
        public int ActiveObserverIndex { get; set; }

        // Light related
        /// <summary>
        /// Min 0, Max 100
        /// </summary>
        public int LightStrength { get; set; }
        public int DiffuseReflectance { get; set; }
        public int AmbientReflectance { get; set; }
        public int SpecularReflectance { get; set; }
        public int Shininess { get; set; }

        // Not sure how to handle
        public int HDR { get; set; }
        public int AverageLogLuminance { get; set; }
        public int Key { get; set; }
    }
}
