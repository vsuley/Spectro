using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using System.Windows.Forms;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace SpectroNamespace
{
    class SceneGraph
    {
        /// <summary>
        /// Model node tree.
        /// </summary>
        public List<ModelNode> ModelGraph { get; private set; }

        public void AssignNewMaterial(string objectName, Material material)
        {
            foreach (ModelNode modelNode in ModelGraph)
            {
                modelNode.AssignNewMaterial(objectName, material);
            }
        }

        public void AssignNewMaterial(int objectId, Material material)
        {
            foreach (ModelNode modelNode in ModelGraph)
            {
                modelNode.AssignNewMaterial(objectId, material);
            }
        }

        /// <summary>
        /// Reads in scene object from files. No other logic.
        /// </summary>
        /// <param name="contentManager">An instance of the content manager that the program is using.</param>
        /// <param name="defaultMaterial">A model cannot render without a material, so assigning a default material to it is mandatory.<</param>
        public void PopulateScene(ContentManager contentManager, Material defaultMaterial)
        {
            // NOTE: This logic is currently hard-coded for simplicity. If at some point in time the scene gets more complex,
            //       then all this code will change to reading an XML scene file, or there will be an external class that specializes in scene 
            //       building that populates the scene graph for us.
            try
            {
                ModelGraph = new List<ModelNode>();
                
                for (int i = 0; i < 4; i++)
                {
                    ModelGraph.Add(new ModelNode());
                    ModelGraph[i].LoadContent(contentManager, "Models\\teapot");
                    ModelGraph[i].Name = "Teapot " + (i+1).ToString();
                    ModelGraph[i].Material = defaultMaterial;
                }

                this.ModelGraph[0].Position = new Vector3(3.5f, 0f, -3.5f);
                this.ModelGraph[1].Position = new Vector3(-3.5f, 0f, -3.5f);
                this.ModelGraph[2].Position = new Vector3(-3.5f, 0f, 3.5f);
                this.ModelGraph[3].Position = new Vector3(3.5f, 0f, 3.5f);
            }
            catch (Exception e)
            {
                MessageBox.Show("Loading model failed with message:" + e.Message);
                throw;
            }
        }

        /// <summary>
        /// Walks through some setup-time logic to prepare the models for rendering. Specifically, in XNA the models need to be updated with an effect
        /// before they can render themselves. This method does that.
        /// </summary>
        /// <param name="renderingEffect">The effect that will be used for rendering.</param>
        public void PrepareForRendering(Effect renderingEffect)
        {
            foreach (ModelNode modelNode in this.ModelGraph)
            {
                modelNode.SetupModelToUseEffect(renderingEffect);
            }
        }

        public void RecalculateColors(LightSource lightSource, Observer observer, bool clipInvisible)
        {
            foreach (ModelNode modelNode in this.ModelGraph)
            {
                modelNode.RecalculateColors(lightSource, observer, clipInvisible);
            }
        }

        public void Render(Matrix world, Matrix view, Matrix projection)
        {
            foreach (ModelNode modelNode in this.ModelGraph)
            {
                modelNode.Render(world, view, projection);
            }
        }

        /// <summary>
        /// Tests all models in the scene graph for intersection with the relative mouse coordinates. 
        /// </summary>
        /// <returns>The ModelNode object that claims to be hit by the mouse</returns>
        /// <remarks>Note that currently this algorithm uses bounding spheres, so for certain objects it will be rather inaccurate.</remarks>
        public ModelNode GetIntersectingModel(Matrix projectionMatrix, Matrix viewMatrix, Matrix worldMatrix, GraphicsDevice graphicsDevice, Vector2 mousePosition)
        {
            // Create 2 positions in screenspace near and far.
            Vector3 nearSource = new Vector3(mousePosition, 0f);
            Vector3 farSource = new Vector3(mousePosition, 1f);
            
            // Get equivalent positions in world space.
            Vector3 nearPoint = graphicsDevice.Viewport.Unproject(nearSource, projectionMatrix, viewMatrix, worldMatrix);
            Vector3 farPoint = graphicsDevice.Viewport.Unproject(farSource, projectionMatrix, viewMatrix, worldMatrix);

            // Find direction for cursor ray.
            Vector3 direction = farPoint - nearPoint;
            direction.Normalize();

            // and then create a new ray using nearPoint as the source.
            Ray cursorRay = new Ray(nearPoint, direction);

            foreach (ModelNode modelNode in ModelGraph)
            {
                ModelNode modelToReturn = modelNode.GetIntersectingModel(cursorRay);

                if (modelToReturn != null)
                    return modelToReturn;
            }

            return null;
        }
    }
}
