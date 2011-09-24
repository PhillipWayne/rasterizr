using System.Collections.Generic;
using Nexus;
using Rasterizr.Core;
using Rasterizr.Effects;

namespace Rasterizr
{
	public class ModelMesh
	{
		private readonly RasterizrDevice _device;

		public List<VertexPositionNormalTexture> Vertices { get; set; }
		public Int32Collection Indices { get; set; }

		public Effect Effect { get; set; }

		public string Name { get; set; }

		public ModelMesh(RasterizrDevice device)
		{
			_device = device;
		}

		public void Draw()
		{
			_device.InputAssembler.InputLayout = VertexPositionNormalTexture.InputLayout;
			_device.InputAssembler.Vertices = Vertices;
			_device.InputAssembler.Indices = Indices;

			foreach (EffectPass pass in Effect.CurrentTechnique.Passes)
			{
				pass.Apply();
				_device.Draw();
			}
		}
	}
}