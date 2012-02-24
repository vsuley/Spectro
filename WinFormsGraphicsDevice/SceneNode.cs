using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace SpectroNamespace
{
    abstract class SceneNode
    {
        // Class variables.
        public static int NodeCount = 0;

        // Identification
        public string Name { get; set; }
        public int NodeId { get; set; }

        public List<SceneNode> SubNodes { get; private set; }

        protected Vector3 _position;
        public Vector3 Position 
        { 
            get
            {
                return _position;
            }

            set
            {
                _position = value;
                _translationMatrix = Matrix.CreateTranslation(_position);
            }
        }

        public Matrix Rotation { get; set; }
        public BoundingSphere BoundingSphere { get; private set; }
        public BoundingBox BoundingBox { get; private set; }

        // Cache
        protected Matrix _worldTransform;
        protected Matrix _translationMatrix;

        public SceneNode()
        {
            SubNodes = new List<SceneNode>();
            Name = "Unnamed node in the scene";
            this.NodeId = SceneNode.NodeCount++;
        }

        public override string ToString()
        {
            return Name;
        }

        public abstract void LoadContent(ContentManager contentManager, string file);
    }
}
