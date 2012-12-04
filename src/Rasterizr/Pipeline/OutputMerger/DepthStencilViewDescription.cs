﻿using System.Runtime.InteropServices;

namespace Rasterizr.Pipeline.OutputMerger
{
	[StructLayout(LayoutKind.Explicit, Pack = 0)]
	public struct DepthStencilViewDescription
	{
		public struct Texture2DResource
		{
			public int MipSlice;
		}

		public struct Texture1DArrayResource
		{
			public int MipSlice;
			public int FirstArraySlice;
			public int ArraySize;
		}

		public struct Texture1DResource
		{
			public int MipSlice;
		}

		public struct Texture2DArrayResource
		{
			public int MipSlice;
			public int FirstArraySlice;
			public int ArraySize;
		}

		public struct Texture2DMultisampledResource
		{
			public int UnusedFieldNothingToDefine;
		}

		public struct Texture2DMultisampledArrayResource
		{
			public int FirstArraySlice;
			public int ArraySize;
		}

		[FieldOffset(0)]
		public Format Format;

		[FieldOffset(4)]
		public DepthStencilViewDimension Dimension;

		[FieldOffset(8)]
		public DepthStencilViewFlags Flags;

		[FieldOffset(12)]
		public Texture1DResource Texture1D;

		[FieldOffset(12)]
		public Texture1DArrayResource Texture1DArray;

		[FieldOffset(12)]
		public Texture2DResource Texture2D;

		[FieldOffset(12)]
		public Texture2DArrayResource Texture2DArray;

		[FieldOffset(12)]
		public Texture2DMultisampledResource Texture2DMS;

		[FieldOffset(12)]
		public Texture2DMultisampledArrayResource Texture2DMSArray;	 
	}
}