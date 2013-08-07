﻿using System;
using Rasterizr.Math;
using Rasterizr.Resources;
using SlimShader;
using SlimShader.VirtualMachine.Resources;

namespace Rasterizr.Pipeline
{
	public partial class ShaderResourceView
	{
		private abstract class InnerResourceView : ITexture
		{
			public Number4 SampleGrad(ISamplerState sampler, ref Number4 location, ref Number4 ddx, ref Number4 ddy)
			{
				return SampleLevel(sampler, ref location, CalculateLevelOfDetail(sampler, ref ddx, ref ddy));
			}

			/// <summary>
			/// 
			/// </summary>
			/// <param name="sampler"></param>
			/// <param name="location"></param>
			/// <param name="lod">A number that specifies the mipmap level. If the value is &lt;=0, the zero'th (biggest map)
			/// is used. The fractional value (if supplied) is used to interpolate between two mipmap levels.</param>
			/// <returns></returns>
            public abstract Number4 SampleLevel(ISamplerState sampler, ref Number4 location, float lod);

            public abstract float CalculateLevelOfDetail(ISamplerState sampler, ref Number4 ddx, ref Number4 ddy);

			public abstract Color4F GetDataIndex(SamplerStateDescription sampler, float u, float v, float w);

			public static InnerResourceView Create(Resource resource, ShaderResourceViewDescription description)
			{
				switch (description.Dimension)
				{
					case ShaderResourceViewDimension.Texture1D:
						throw new NotImplementedException();
					case ShaderResourceViewDimension.Texture1DArray:
						throw new NotImplementedException();
					case ShaderResourceViewDimension.Texture2D:
						return new Texture2DView((Texture2D) resource, description.Texture2D);
					case ShaderResourceViewDimension.Texture2DArray:
						throw new NotImplementedException();
					case ShaderResourceViewDimension.Texture2DMultisampled:
						throw new NotImplementedException();
					case ShaderResourceViewDimension.Texture2DMultisampledArray:
						throw new NotImplementedException();
					case ShaderResourceViewDimension.Texture3D:
						throw new NotImplementedException();
					case ShaderResourceViewDimension.TextureCube:
						throw new NotImplementedException();
					case ShaderResourceViewDimension.TextureCubeArray:
						throw new NotImplementedException();
					default:
						throw new ArgumentOutOfRangeException();
				}
			}
		}
	}
}