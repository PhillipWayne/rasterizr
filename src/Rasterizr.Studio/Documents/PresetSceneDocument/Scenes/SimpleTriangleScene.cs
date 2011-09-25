using Nexus;
using Nexus.Graphics.Cameras;
using Rasterizr.Core;
using Rasterizr.Effects;

namespace Rasterizr.Studio.Documents.PresetSceneDocument.Scenes
{
	public class SimpleTriangleScene : PresetSceneBase
	{
		public override void Draw(RasterizrDevice device)
		{
			device.ClearDepthBuffer(1);
			device.ClearRenderTarget(ColorsF.Gray);

			Camera camera = new PerspectiveCamera
			{
				FieldOfView = MathUtility.PI_OVER_4,
				FarPlaneDistance = 100.0f,
				NearPlaneDistance = 1.0f,
				Position = new Point3D(0, 0, 10),
				LookDirection = Vector3D.Forward
			};

			device.InputAssembler.InputLayout = VertexPositionColor.InputLayout;
			device.InputAssembler.Vertices = new[]
			{
				new VertexPositionColor(new Point3D(-1, 0, 0), ColorsF.Red),
				new VertexPositionColor(new Point3D(1, 0, 0), ColorsF.Blue),
				new VertexPositionColor(new Point3D(0, 2, 0), ColorsF.Green),
			};

			var effect = new BasicEffect(device, device.InputAssembler.InputLayout)
			{
				Projection = camera.GetProjectionMatrix(device.Rasterizer.Viewport.AspectRatio),
				View = camera.GetViewMatrix()
			};

			foreach (EffectPass pass in effect.CurrentTechnique.Passes)
			{
				pass.Apply();
				device.Draw();
			}
		}
	}
}