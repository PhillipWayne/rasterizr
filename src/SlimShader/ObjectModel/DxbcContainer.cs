using System.Collections.Generic;
using System.Linq;
using System.Text;
using SlimShader.IO;
using SlimShader.Parser;

namespace SlimShader.ObjectModel
{
	public class DxbcContainer
	{
		public DxbcContainerHeader Header { get; private set; }
		public List<DxbcChunk> Chunks { get; private set; }

		public DxbcContainer()
		{
			Chunks = new List<DxbcChunk>();
		}

		public static DxbcContainer Parse(BytecodeReader reader)
		{
			var container = new DxbcContainer();

			uint fourCc = reader.ReadUInt32();
			if (fourCc != "DXBC".ToFourCc())
				throw new ParseException("Invalid FourCC");

			var uniqueKey = new uint[4];
			uniqueKey[0] = reader.ReadUInt32();
			uniqueKey[1] = reader.ReadUInt32();
			uniqueKey[2] = reader.ReadUInt32();
			uniqueKey[3] = reader.ReadUInt32();

			container.Header = new DxbcContainerHeader
			{
				FourCc = fourCc,
				UniqueKey = uniqueKey,
				One = reader.ReadUInt32(),
				TotalSize = reader.ReadUInt32(),
				ChunkCount = reader.ReadUInt32()
			};

			for (uint i = 0; i < container.Header.ChunkCount; i++)
			{
				uint chunkOffset = reader.ReadUInt32();
				var chunkReader = reader.CopyAtOffset((int) chunkOffset);
				container.Chunks.Add(DxbcChunk.ParseChunk(chunkReader));
			}

			return container;
		}

		public override string ToString()
		{
			var sb = new StringBuilder();
			sb.AppendLine("// ");
			sb.AppendLine("// Generated by Shaderflect " + GetType().Assembly.GetName().Version);
			sb.AppendLine("// ");
			sb.AppendLine("// ");
			sb.AppendLine("// ");

			// TODO: Remove these hardcoded lines once we have the proper reflected values.

			sb.AppendLine(@"//
//
// Buffer Definitions: 
//
// cbuffer cbuf0
// {
//
//   float4 cool;                       // Offset:    0 Size:    16
//   int4 zeek;                         // Offset:   16 Size:    16
//   int2 arr[127];                     // Offset:   32 Size:  2024
//
// }
//
//
// Resource Bindings:
//
// Name                                 Type  Format         Dim Slot Elements
// ------------------------------ ---------- ------- ----------- ---- --------
// samp0                             sampler      NA          NA    0        1
// samp1                             sampler      NA          NA    1        1
// tex0                              texture  float4          2d    0        1
// tex1                              texture  float4        cube    1        1
// tex2                              texture  float4          3d    2        1
// tex3                              texture  float4       2dMS2    3        1
// tex4                              texture  float4          2d    4        2
// cbuf0                             cbuffer      NA          NA    0        1
//
//
//
// Input signature:
//
// Name                 Index   Mask Register SysValue Format   Used
// -------------------- ----- ------ -------- -------- ------ ------
// TEXCOORD                 0   xyzw        0     NONE  float   xyzw
// TEXCOORD                 1   xyzw        1     NONE  float   x   
// SV_POSITION              0   xyzw        2      POS  float   x   
//
//
// Output signature:
//
// Name                 Index   Mask Register SysValue Format   Used
// -------------------- ----- ------ -------- -------- ------ ------
// SV_TARGET                0   xyzw        0   TARGET  float   xyzw
//");

			var shaderProgramChunk = Chunks.OfType<ShaderProgramChunk>().FirstOrDefault();
			if (shaderProgramChunk != null)
				sb.Append(shaderProgramChunk);

			return sb.ToString();
		}
	}
}