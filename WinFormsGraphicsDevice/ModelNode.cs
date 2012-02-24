using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Windows.Forms;

namespace SpectroNamespace
{
    class ModelNode : SceneNode
    {
        public Material Material { get; set; }
        public Vector3 FinalTristimulus { get; set; }
        public Vector4 FinalRGB { get; set; }
        public Vector4 MaterialRGB { get; set; }
        public Model Model { get; private set; }
        public Effect Effect { get; set; }

        public override void LoadContent(ContentManager contentManager, string file)
        {
            Model = contentManager.Load<Model>(file);
        }

        public void AssignNewMaterial(string objectName, Material material)
        {
            if (this.Name.Equals(objectName))
            {
                this.Material = material;
            }

            foreach (ModelNode modelNode in this.SubNodes)
            {
                modelNode.AssignNewMaterial(objectName, material);
            }
        }

        public void AssignNewMaterial(int objectId, Material material)
        {
            if (this.NodeId == objectId)
            {
                this.Material = material;
            }

            foreach (ModelNode modelNode in this.SubNodes)
            {
                modelNode.AssignNewMaterial(objectId, material);
            }
        }

        public void SetupModelToUseEffect(Effect effect)
        {
            // Modify model to use custom effect.
            this.Effect = effect;

            foreach (ModelMesh mesh in this.Model.Meshes)
            {
                foreach (ModelMeshPart part in mesh.MeshParts)
                {
                    part.Effect = effect;
                }
            }

            // Also pass on the change to all subnodes.
            foreach (ModelNode modelNode in this.SubNodes)
            {
                modelNode.SetupModelToUseEffect(effect);
            }
        }

        public void Render(Matrix world, Matrix view, Matrix projection)
        {
            _worldTransform = Matrix.Multiply(world, _translationMatrix);

            this.Effect.Parameters["FinalRGB"].SetValue(this.FinalRGB);
            this.Effect.Parameters["MaterialRGB"].SetValue(this.MaterialRGB);

            // Loop through each mesh in the model
            foreach (ModelMesh mesh in this.Model.Meshes)
            {
                // A mesh has multiple parts and each part has an effect. Setup the matrices for every effect.
                foreach (Effect effect in mesh.Effects)
                {
                    effect.Parameters["World"].SetValue(_worldTransform);
                    effect.Parameters["View"].SetValue(view);
                    effect.Parameters["Projection"].SetValue(projection);
                }

                // Now render the mesh!
                //
                mesh.Draw();
            }
        }

        public void RecalculateColors(LightSource lightSource, Observer observer, bool clipInvisible)
        {
            try
            {
                // Calculate RGB values
                this.MaterialRGB = Utilities.GetEquivalentRGB(this.Material);
                    
                // Calculate the tristimulus values.
                this.FinalTristimulus = Utilities.CalculateTristimulusValues(lightSource, this.Material, observer, clipInvisible);
                this.FinalRGB = Utilities.CalculateRGBfromXYZ(FinalTristimulus);

                // Not sure why I'm having to do this, but for now sometimes the scene renders like a ghost, so make alpha 1.
                this.FinalRGB = new Vector4(FinalRGB.X, FinalRGB.Y, FinalRGB.Z, 1f);
                this.MaterialRGB = new Vector4(MaterialRGB.X, MaterialRGB.Y, MaterialRGB.Z, 1f);
            }
            catch (Exception e)
            {
                MessageBox.Show("Color calculations because " + e.Message);
                throw;
            }
        }

        public ModelNode GetIntersectingModel(Ray ray)
        {
            foreach (ModelMesh mesh in this.Model.Meshes)
            {
                BoundingSphere sphere = mesh.BoundingSphere;
                sphere.Center = Vector3.Transform(sphere.Center, this._worldTransform);

                if (sphere.Intersects(ray) != null)
                {
                    return this;
                }
            }

            foreach (ModelNode modelNode in this.SubNodes)
            {
                ModelNode modelToReturn = modelNode.GetIntersectingModel(ray);

                if (modelToReturn != null)
                    return modelToReturn;
            }

            return null;
        }
    }
}
