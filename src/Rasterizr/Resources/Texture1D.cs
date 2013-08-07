﻿using System;
using Rasterizr.Math;

namespace Rasterizr.Resources
{
	public class Texture1D : TextureBase
	{
		internal class Texture1DSubresource : TextureSubresource
		{
			public int Width { get; private set; }

			public Texture1DSubresource(int width)
				: base(width)
			{
				Width = width;
			}

			public Color4F GetData(int x)
			{
				return Data[x];
			}

            public void SetData(int x, ref Color4F value)
            {
                Data[x] = value;
            }
		}

		private readonly Texture1DDescription _description;
		private readonly Texture1DSubresource[][] _subresources;

		public Texture1DDescription Description
		{
			get { return _description; }
		}

		internal override ResourceType ResourceType
		{
			get { return ResourceType.Texture1D; }
		}

		internal Texture1D(Device device, Texture1DDescription description)
			: base(device)
		{
			_description = description;

			_subresources = new Texture1DSubresource[description.ArraySize][];
			int mipMapCount = MipMapUtility.CalculateMipMapCount(description.MipLevels, description.Width);
			for (int i = 0; i < description.ArraySize; i++)
				_subresources[i] = MipMapUtility.CreateMipMaps(mipMapCount,
					description.Width);
		}

        public override Color4F[] GetData(int subresource)
        {
            int mipSlice, arrayIndex;
            CalculateArrayMipSlice(subresource, _subresources[0].Length, out mipSlice, out arrayIndex);

            return _subresources[arrayIndex][mipSlice].Data;
        }

		public override void SetData(int subresource, Color4F[] data)
		{
			int mipSlice, arrayIndex;
			CalculateArrayMipSlice(subresource, _subresources[0].Length, out mipSlice, out arrayIndex);

			Array.Copy(data, _subresources[arrayIndex][mipSlice].Data, data.Length);
		}

		internal Texture1DSubresource GetSubresource(int arrayIndex, int mipSlice)
		{
			return _subresources[arrayIndex][mipSlice];
		}

		internal void GetDimensions(int mipLevel, out int width, out int numberOfLevels)
		{
			GetDimensions(mipLevel, out width);
			numberOfLevels = _subresources[0].Length;
		}

		internal void GetDimensions(int mipLevel, out int width)
		{
			var subresource = _subresources[0][mipLevel];
			width = subresource.Width;
		}

		internal void GetDimensions(out int width)
		{
			GetDimensions(0, out width);
		}
	}
}