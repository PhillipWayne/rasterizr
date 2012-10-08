﻿using System.ComponentModel.Composition;
using System.Windows.Media.Imaging;
using Gemini.Framework;
using Gemini.Modules.Output;
using Nexus.Graphics;
using Rasterizr.Core;
using Rasterizr.Core.Diagnostics;
using Rasterizr.Core.OutputMerger;

namespace Rasterizr.Studio.Framework
{
	public abstract class RenderedDocumentBase : Document
	{
		[Import]
		private IOutput _output;

		private readonly RasterizrDevice _device;
		private SwapChain _swapChain;

		protected RasterizrDevice Device
		{
			get { return _device; }
		}

		protected SwapChain SwapChain
		{
			get { return _swapChain; }
		}

		private WriteableBitmap _outputBitmap;
		public WriteableBitmap OutputBitmap
		{
			get { return _outputBitmap; }
			set
			{
				_outputBitmap = value;
				if (value != null)
				{
					RecreateSwapChain();
					Draw();
				}
			}
		}

		protected RenderedDocumentBase()
 		{
			_device = new RasterizrDevice();
			_device.Loggers.Add(new TextWriterGraphicsLogger(_output.Writer));
 		}

		private void RecreateSwapChain()
		{
			_swapChain = new SwapChain(_device, OutputBitmap, (int)OutputBitmap.Width, (int)OutputBitmap.Height, 1);
			_device.Rasterizer.Viewport = new Viewport3D(0, 0, (int)OutputBitmap.Width, (int)OutputBitmap.Height);
			_device.OutputMerger.RenderTarget = new RenderTargetView(_swapChain.GetBuffer());
		}

		protected abstract void Draw();
	}
}