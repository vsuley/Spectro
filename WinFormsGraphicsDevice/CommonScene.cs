using System;
using System.Diagnostics;
using System.Windows.Forms;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace SpectroNamespace
{
    abstract class CommonScene : GraphicsDeviceControl
    {
        // References to important program level objects.
        public RenderSettings RenderSettings { get; set; }
        public DataManager DataManager { get; set; }
        
        // Some important properties and fields.
        public SceneGraph SceneGraph { get; set; }
        public bool Recompute { get; set; }
        public bool Animate { get; set; }

        // General
        protected ContentManager _contentManager;
        protected Effect _effect;
        protected Stopwatch _timer;
        protected Matrix _projectionMatrix;
        protected Matrix _worldMatrix;
        protected double _timeSpentCalculating;
        protected double _timeSpentRendering;

        // Sprite related
        protected SpriteBatch _spriteBatch;
        protected SpriteFont _font;

        // Set the position of the camera in world space, for our view matrix.
        protected Vector3 _cameraPosition;
        protected Matrix _lookAtMatrix;

        public CommonScene()
        {
            _projectionMatrix = Matrix.Identity;
            _worldMatrix = Matrix.Identity;
            _lookAtMatrix = Matrix.Identity;
        }

        /// <summary>
        /// Simply loads in content from content files. No other setup logic.
        /// </summary>
        public override void LoadContent()
        {
            try
            {
                // Setup the content manager.
                this._contentManager = new ContentManager(Services, "Content");

                // Setup rendering effect
                this._effect = new BasicEffect(this.GraphicsDevice);

                // Populate the scene graph
                this.SceneGraph = new SceneGraph();
                this.SceneGraph.PopulateScene(_contentManager, this.DataManager.Materials[0]);

                // Font.
                _spriteBatch = new SpriteBatch(GraphicsDevice);
                _font = _contentManager.Load<SpriteFont>("hudFont");
            }
            catch (ContentLoadException e)
            {
                MessageBox.Show(e.Message);
                throw;
            }
        }

        /// <summary>
        /// Walks through some setup steps to prepare the models etc. for rendering.
        /// </summary>
        virtual public void PrepareForRendering()
        {
            // Setup projection matrix.
            bool orthographic = false;

            if (orthographic)
            {
                float projectionHeight = 40f;
                float projectionDepth = 1000f;
                _projectionMatrix = Matrix.CreateOrthographic(GraphicsDevice.Viewport.AspectRatio * projectionHeight, projectionHeight, 1f, projectionDepth);
                
                // From an aesthetic point of view, our camera position is dependent on our projection.
                _cameraPosition = new Vector3(0.0f, 0.0f, 200.0f);
            }
            else
            {
                _projectionMatrix = Matrix.CreatePerspectiveFieldOfView(MathHelper.ToRadians(30.0f), GraphicsDevice.Viewport.AspectRatio, 1.0f, 500.0f);

                // From an aesthetic point of view, our camera position is dependent on our projection.
                _cameraPosition = new Vector3(20.0f, 20.0f, 20.0f);
            }

            // Prepare the lookat matrix.
            _lookAtMatrix = Matrix.CreateLookAt(_cameraPosition, Vector3.Zero, Vector3.Up);

            // Prepare the scenegraph
            this.SceneGraph.PrepareForRendering(_effect);

            // Set renderstates.
            GraphicsDevice.RasterizerState = RasterizerState.CullNone;

            // Start the animation _timer.
            _timer = Stopwatch.StartNew();

            // Hook the idle event to constantly redraw our animation.
            Application.Idle += delegate { Invalidate(); };

            // Make sure you compute at the start.
            this.Recompute = true;
        }

        /// <summary>
        /// Draws the control.
        /// In our case the control is a scene with ONE complex geometric object. Please remember that this method is NOT
        /// optimized for rendering a hierarchy of elements. It does NOT handle bones system correctly.
        /// </summary>
        protected override void Draw()
        {
            Stopwatch renderTimer = new Stopwatch();
            renderTimer.Start();

            // Check if any of the scene elements have been changed.
            if (this.Recompute)
            {
                this.RecomputeColorValues();
                this.Recompute = false;
            }

            // Clear
            GraphicsDevice.Clear(Color.Gray);

            // Spin the camera around the models. We want to spin by a certain amount every millisecond
            float time = (float)_timer.ElapsedMilliseconds;
            float rotationAmount = this.Animate? time * 0.00007f : 0.0f;
            _timer.Restart();

            _lookAtMatrix = Matrix.Multiply(Matrix.CreateRotationY(rotationAmount), _lookAtMatrix);

            // Render the scene graph
            _worldMatrix = Matrix.Identity;
            this.SceneGraph.Render(_worldMatrix, _lookAtMatrix, _projectionMatrix);

            renderTimer.Stop();
            _timeSpentRendering = (_timeSpentRendering + renderTimer.Elapsed.Duration().TotalMilliseconds) / 2;

            _spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.LinearWrap, DepthStencilState.Default, RasterizerState.CullNone);
            _spriteBatch.DrawString(_font, "Color calculations took " + _timeSpentCalculating.ToString() + " milliseconds.", new Vector2(10f, 10f), Color.White);
            // _spriteBatch.DrawString(_font, "Rendering took " + _timeSpentRendering.ToString() + " milliseconds.", new Vector2(10f, 30f), Color.White);
            _spriteBatch.End();

        } // The model has been rendered!
        
        protected void RecomputeColorValues()
        {
            // For brevity
            DataManager dm = this.DataManager;
            RenderSettings rs = this.RenderSettings;

            LightSource lightSource = dm.LightSources[rs.ActiveLightSourceIndex];
            Observer observer = dm.Observers[rs.ActiveObserverIndex];
            // Material material = dm.Materials[rs.ActiveMaterialIndex];

            Stopwatch colorCalcTimer = new Stopwatch();
            colorCalcTimer.Reset();
            colorCalcTimer.Start();

            this.SceneGraph.RecalculateColors(lightSource, observer, rs.ClipInvisible);

            colorCalcTimer.Stop();
            _timeSpentCalculating = colorCalcTimer.Elapsed.Duration().TotalMilliseconds;

            // Update light intensity.
            // TODO: in the future, you might want to consider creating a new method that only deals with non-color changes
            // so that we don't have to do all the hairy math required for color for simply light changes.
            _effect.Parameters["LightStrength"].SetValue((float)rs.LightStrength / 100.0f);

            // Light RGB
            _effect.Parameters["LightColor"].SetValue(Utilities.GetEquivalentRGB(lightSource));

            // Reinhard tone mapping variables.
            //_effect.Parameters["AverageLogLuminance"].SetValue();
            //_effect.Parameters["Key"].SetValue();
            //_effect.Parameters["Lw_a"].SetValue();
            //_effect.Parameters["LogTerm"].SetValue();
        }

        public ModelNode GetIntersectingModel()
        {
            float titleBarHeight = 30f;
            float frameWidth = 10f;

            MouseState mouseState = Mouse.GetState();
            Vector2 mousePosition = new Vector2(mouseState.X - this.Parent.Bounds.Left - this.Bounds.Left - frameWidth, 
                                                mouseState.Y - this.Parent.Bounds.Top - this.Bounds.Top - titleBarHeight);

            return SceneGraph.GetIntersectingModel(this._projectionMatrix, this._lookAtMatrix, this._worldMatrix, this.GraphicsDevice, mousePosition);
        }

        public void UpdateSelectedObjectMaterial(string objectName, Material material)
        {
            this.SceneGraph.AssignNewMaterial(objectName, material);
        }
    }
}
