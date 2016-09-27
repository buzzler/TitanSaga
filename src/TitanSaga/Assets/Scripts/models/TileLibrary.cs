using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Xml;
using System.Xml.Serialization;
using System.IO;

namespace TileLib
{
	public	class TileWallConst
	{
		public	const uint NONE = 0x00;
		public	const uint U = 0x01;
		public	const uint L = 0x04;
		public	const uint UL = 0x10;

		public	const uint U_L = U | L;
	}

	public	class TileSimpleConst
	{
		public	const uint NONE = 0x00;
		public	const uint U = 0x01;
		public	const uint D = 0x02;
		public	const uint L = 0x04;
		public	const uint R = 0x08;
		public	const uint UL = 0x10;
		public	const uint UR = 0x20;
		public	const uint DL = 0x40;
		public	const uint DR = 0x80;

		public	const uint U_D = U | D;
		public	const uint U_L = U | L;
		public	const uint U_R = U | R;
		public	const uint D_L = D | L;
		public	const uint D_R = D | R;
		public	const uint L_R = L | R;
		public	const uint U_D_L = U | D | L;
		public	const uint U_D_R = U | D | R;
		public	const uint U_L_R = U | L | R;
		public	const uint D_L_R = D | L | R;
		public	const uint U_D_L_R = U | D | L | R;
	}

	public	class TileComplexConst
	{
		public	const uint NONE = 0x00;
		public	const uint U = 0x01;
		public	const uint D = 0x02;
		public	const uint L = 0x04;
		public	const uint R = 0x08;
		public	const uint UL = 0x10;
		public	const uint UR = 0x20;
		public	const uint DL = 0x40;
		public	const uint DR = 0x80;

		public	const uint U_UL = U | UL;
		public	const uint U_UR = U | UR;
		public	const uint U_UL_UR = U_UL | UR;
		public	const uint D_DL = D | DL;
		public	const uint D_DR = D | DR;
		public	const uint D_DL_DR = D_DL | DR;
		public	const uint L_UL = L | UL;
		public	const uint L_DL = L | DL;
		public	const uint L_UL_DL = L_UL | DL;
		public	const uint R_UR = R | UR;
		public	const uint R_DR = R | DR;
		public	const uint R_UR_DR = R_UR | DR;
		public	const uint U_DL = U | DL;
		public	const uint U_DL_UL = U_DL | UL;
		public	const uint U_DL_UR = U_DL | UR;
		public	const uint U_DL_UL_UR = U_DL_UL | UR;
		public	const uint U_DR = U | DR;
		public	const uint U_DR_UL = U_DR | UL;
		public	const uint U_DR_UR = U_DR | UR;
		public	const uint U_DR_UL_UR = U_DR_UL | UR;
		public	const uint D_UR = D | UR;
		public	const uint D_UR_DL = D_UR | DL;
		public	const uint D_UR_DR = D_UR | DR;
		public	const uint D_UR_DL_DR = D_UR_DL | DR;
		public	const uint D_UL = D | UL;
		public	const uint D_UL_DL = D_UL | DL;
		public	const uint D_UL_DR = D_UL | DR;
		public	const uint D_UL_DL_DR = D_UL_DL | DR;
		public	const uint L_UR = L | UR;
		public	const uint L_UR_UL = L_UR | UL;
		public	const uint L_UR_DL = L_UR | DL;
		public	const uint L_UR_UL_DL = L_UR_UL | DL;
		public	const uint L_DR = L | DR;
		public	const uint L_DR_UL = L_DR | UL;
		public	const uint L_DR_DL = L_DR | DL;
		public	const uint L_DR_UL_DL = L_DR_UL | DL;
		public	const uint R_DL = R | DL;
		public	const uint R_DL_UR = R_DL | UR;
		public	const uint R_DL_DR = R_DL | DR;
		public	const uint R_DL_UR_DR = R_DL_UR | DR;
		public	const uint R_UL = R | UL;
		public	const uint R_UL_UR = R_UL | UR;
		public	const uint R_UL_DR = R_UL | DR;
		public	const uint R_UL_UR_DR = R_UL_UR | DR;
		public	const uint U_DL_DR = U_DL | DR;
		public	const uint U_DL_DR_UL = U_DL_DR | UL;
		public	const uint U_DL_DR_UR = U_DL_DR | UR;
		public	const uint U_DL_DR_UL_UR = U_DL_DR_UL | UR;
		public	const uint D_UL_UR = D_UL | UR;
		public	const uint D_UL_UR_DL = D_UL_UR | DL;
		public	const uint D_UL_UR_DR = D_UL_UR | DR;
		public	const uint D_UL_UR_DL_DR = D_UL_UR_DL | DR;
		public	const uint L_UR_DR = L_UR | DR;
		public	const uint L_UR_DR_UL = L_UR_DR | UL;
		public	const uint L_UR_DR_DL = L_UR_DR | DL;
		public	const uint L_UR_DR_UL_DL = L_UR_DR_UL | DL;
		public	const uint R_UL_DL = R_UL | DL;
		public	const uint R_UL_DL_UR = R_UL_DL | UR;
		public	const uint R_UL_DL_DR = R_UL_DL | DR;
		public	const uint R_UL_DL_UR_DR = R_UL_DL_UR | DR;
		public	const uint DL_DR = DL | DR;
		public	const uint UL_UR = UL | UR;
		public	const uint UL_DR = UL | DR;
		public	const uint DL_UR = DL | UR;
		public	const uint UL_DL = UL | DL;
		public	const uint UR_DR = UR | DR;
		public	const uint U_R = U | R;
		public	const uint U_R_UL = U_R | UL;
		public	const uint U_R_UR = U_R | UR;
		public	const uint U_R_DR = U_R | DR;
		public	const uint U_R_UL_UR = U_R_UL | UR;
		public	const uint U_R_UL_DR = U_R_UL | DR;
		public	const uint U_R_UR_DR = U_R_UR | DR;
		public	const uint U_R_UL_UR_DR = U_R_UL_UR | DR;
		public	const uint D_R = D | R;
		public	const uint D_R_DL = D_R | DL;
		public	const uint D_R_UR = D_R | UR;
		public	const uint D_R_DR = D_R | DR;
		public	const uint D_R_DL_UR = D_R_DL | UR;
		public	const uint D_R_DL_DR = D_R_DL | DR;
		public	const uint D_R_UR_DR = D_R_UR | DR;
		public	const uint D_R_DL_UR_DR = D_R_DL_UR | DR;
		public	const uint D_L = D | L;
		public	const uint D_L_DL = D_L | DL;
		public	const uint D_L_UL = D_L | UL;
		public	const uint D_L_DR = D_L | DR;
		public	const uint D_L_DL_UL = D_L_DL | UL;
		public	const uint D_L_DL_DR = D_L_DL | DR;
		public	const uint D_L_UL_DR = D_L_UL | DR;
		public	const uint D_L_DL_UL_DR = D_L_DL_UL | DR;
		public	const uint U_L = U | L;
		public	const uint U_L_DL = U_L | DL;
		public	const uint U_L_UL = U_L | UL;
		public	const uint U_L_UR = U_L | UR;
		public	const uint U_L_DL_UL = U_L_DL | UL;
		public	const uint U_L_DL_UR = U_L_DL | UR;
		public	const uint U_L_UL_UR = U_L_UL | UR;
		public	const uint U_L_DL_UL_UR = U_L_DL_UL | UR;
		public	const uint U_D = U | D;
		public	const uint U_D_UL = U_D | UL;
		public	const uint U_D_UR = U_D | UR;
		public	const uint U_D_DL = U_D | DL;
		public	const uint U_D_DR = U_D | DR;
		public	const uint U_D_UL_UR = U_D_UL | UR;
		public	const uint U_D_UL_DL = U_D_UL | DL;
		public	const uint U_D_UL_DR = U_D_UL | DR;
		public	const uint U_D_UR_DL = U_D_UR | DL;
		public	const uint U_D_UR_DR = U_D_UR | DR;
		public	const uint U_D_DL_DR = U_D_DL | DR;
		public	const uint U_D_UL_UR_DL = U_D_UL_UR | DL;
		public	const uint U_D_UL_UR_DR = U_D_UL_UR | DR;
		public	const uint U_D_UL_DL_DR = U_D_UL_DL | DR;
		public	const uint U_D_UR_DL_DR = U_D_UR_DL | DR;
		public	const uint U_D_UL_UR_DL_DR = U_D_UL_UR_DL | DR;
		public	const uint L_R = L | R;
		public	const uint L_R_UL = L_R | UL;
		public	const uint L_R_UR = L_R | UR;
		public	const uint L_R_DL = L_R | DL;
		public	const uint L_R_DR = L_R | DR;
		public	const uint L_R_UL_UR = L_R_UL | UR;
		public	const uint L_R_UL_DL = L_R_UL | DL;
		public	const uint L_R_UL_DR = L_R_UL | DR;
		public	const uint L_R_UR_DL = L_R_UR | DL;
		public	const uint L_R_UR_DR = L_R_UR | DR;
		public	const uint L_R_DL_DR = L_R_DL | DR;
		public	const uint L_R_UL_UR_DL = L_R_UL_UR | DL;
		public	const uint L_R_UL_UR_DR = L_R_UL_UR | DR;
		public	const uint L_R_UL_DL_DR = L_R_UL_DL | DR;
		public	const uint L_R_UR_DL_DR = L_R_UR_DL | DR;
		public	const uint L_R_UL_UR_DL_DR = L_R_UL_UR_DL | DR;
		public	const uint D_R_UL = D_R | UL;
		public	const uint D_R_UL_UR = D_R_UL | UR;
		public	const uint D_R_UL_DL = D_R_UL | DL;
		public	const uint D_R_UL_DR = D_R_UL | DR;
		public	const uint D_R_UL_UR_DL = D_R_UL_UR | DL;
		public	const uint D_R_UL_UR_DR = D_R_UL_UR | DR;
		public	const uint D_R_UL_DL_DR = D_R_UL_DL | DR;
		public	const uint D_R_UL_UR_DL_DR = D_R_UL_UR_DL | DR;
		public	const uint D_L_UR = D_L | UR;
		public	const uint D_L_UR_UL = D_L_UR | UL;
		public	const uint D_L_UR_DL = D_L_UR | DL;
		public	const uint D_L_UR_DR = D_L_UR | DR;
		public	const uint D_L_UR_UL_DL = D_L_UR_UL | DL;
		public	const uint D_L_UR_UL_DR = D_L_UR_UL | DR;
		public	const uint D_L_UR_DL_DR = D_L_UR_DL | DR;
		public	const uint D_L_UR_UL_DL_DR = D_L_UR_UL_DL | DR;
		public	const uint U_L_DR = U_L | DR;
		public	const uint U_L_DR_UL = U_L_DR | UL;
		public	const uint U_L_DR_DL = U_L_DR | DL;
		public	const uint U_L_DR_UR = U_L_DR | UR;
		public	const uint U_L_DR_UL_DL = U_L_DR_UL | DL;
		public	const uint U_L_DR_UL_UR = U_L_DR_UL | UR;
		public	const uint U_L_DR_DL_UR = U_L_DR_DL | UR;
		public	const uint U_L_DR_UL_DL_UR = U_L_DR_UL_DL | UR;
		public	const uint U_R_DL = U_R | DL;
		public	const uint U_R_DL_UL = U_R_DL | UL;
		public	const uint U_R_DL_DR = U_R_DL | DR;
		public	const uint U_R_DL_UR = U_R_DL | UR;
		public	const uint U_R_DL_UL_DR = U_R_DL_UL | DR;
		public	const uint U_R_DL_UL_UR = U_R_DL_UL | UR;
		public	const uint U_R_DL_DR_UR = U_R_DL_DR | UR;
		public	const uint U_R_DL_UL_DR_UR = U_R_DL_UL_DR | UR;
		public	const uint D_L_R = D_L | R;
		public	const uint D_L_R_UL = D_L_R | UL;
		public	const uint D_L_R_UR = D_L_R | UR;
		public	const uint D_L_R_DL = D_L_R | DL;
		public	const uint D_L_R_DR = D_L_R | DR;
		public	const uint D_L_R_UL_UR = D_L_R_UL | UR;
		public	const uint D_L_R_UL_DL = D_L_R_UL | DL;
		public	const uint D_L_R_UL_DR = D_L_R_UL | DR;
		public	const uint D_L_R_UR_DL = D_L_R_UR | DL;
		public	const uint D_L_R_UR_DR = D_L_R_UR | DR;
		public	const uint D_L_R_DL_DR = D_L_R_DL | DR;
		public	const uint D_L_R_UL_UR_DL = D_L_R_UL_UR | DL;
		public	const uint D_L_R_UL_UR_DR = D_L_R_UL_UR | DR;
		public	const uint D_L_R_UL_DL_DR = D_L_R_UL_DL | DR;
		public	const uint D_L_R_UR_DL_DR = D_L_R_UR_DL | DR;
		public	const uint D_L_R_UL_UR_DL_DR = D_L_R_UL_UR_DL | DR;
		/*
		case TileComplexConst.U|TileComplexConst.L|TileComplexConst.R:
		case TileComplexConst.U|TileComplexConst.L|TileComplexConst.R|TileComplexConst.UL:
		case TileComplexConst.U|TileComplexConst.L|TileComplexConst.R|TileComplexConst.UR:
		case TileComplexConst.U|TileComplexConst.L|TileComplexConst.R|TileComplexConst.DL:
		case TileComplexConst.U|TileComplexConst.L|TileComplexConst.R|TileComplexConst.DR:
		case TileComplexConst.U|TileComplexConst.L|TileComplexConst.R|TileComplexConst.UL|TileComplexConst.UR:
		case TileComplexConst.U|TileComplexConst.L|TileComplexConst.R|TileComplexConst.UL|TileComplexConst.DL:
		case TileComplexConst.U|TileComplexConst.L|TileComplexConst.R|TileComplexConst.UL|TileComplexConst.DR:
		case TileComplexConst.U|TileComplexConst.L|TileComplexConst.R|TileComplexConst.UR|TileComplexConst.DL:
		case TileComplexConst.U|TileComplexConst.L|TileComplexConst.R|TileComplexConst.UR|TileComplexConst.DR:
		case TileComplexConst.U|TileComplexConst.L|TileComplexConst.R|TileComplexConst.DL|TileComplexConst.DR:
		case TileComplexConst.U|TileComplexConst.L|TileComplexConst.R|TileComplexConst.UL|TileComplexConst.UR|TileComplexConst.DL:
		case TileComplexConst.U|TileComplexConst.L|TileComplexConst.R|TileComplexConst.UL|TileComplexConst.UR|TileComplexConst.DR:
		case TileComplexConst.U|TileComplexConst.L|TileComplexConst.R|TileComplexConst.UL|TileComplexConst.DL|TileComplexConst.DR:
		case TileComplexConst.U|TileComplexConst.L|TileComplexConst.R|TileComplexConst.UR|TileComplexConst.DL|TileComplexConst.DR:
		case TileComplexConst.U|TileComplexConst.L|TileComplexConst.R|TileComplexConst.UL|TileComplexConst.UR|TileComplexConst.DL|TileComplexConst.DR:
		case TileComplexConst.U|TileComplexConst.D|TileComplexConst.R:
		case TileComplexConst.U|TileComplexConst.D|TileComplexConst.R|TileComplexConst.UL:
		case TileComplexConst.U|TileComplexConst.D|TileComplexConst.R|TileComplexConst.UR:
		case TileComplexConst.U|TileComplexConst.D|TileComplexConst.R|TileComplexConst.DL:
		case TileComplexConst.U|TileComplexConst.D|TileComplexConst.R|TileComplexConst.DR:
		case TileComplexConst.U|TileComplexConst.D|TileComplexConst.R|TileComplexConst.UL|TileComplexConst.UR:
		case TileComplexConst.U|TileComplexConst.D|TileComplexConst.R|TileComplexConst.UL|TileComplexConst.DL:
		case TileComplexConst.U|TileComplexConst.D|TileComplexConst.R|TileComplexConst.UL|TileComplexConst.DR:
		case TileComplexConst.U|TileComplexConst.D|TileComplexConst.R|TileComplexConst.UR|TileComplexConst.DL:
		case TileComplexConst.U|TileComplexConst.D|TileComplexConst.R|TileComplexConst.UR|TileComplexConst.DR:
		case TileComplexConst.U|TileComplexConst.D|TileComplexConst.R|TileComplexConst.DL|TileComplexConst.DR:
		case TileComplexConst.U|TileComplexConst.D|TileComplexConst.R|TileComplexConst.UL|TileComplexConst.UR|TileComplexConst.DL:
		case TileComplexConst.U|TileComplexConst.D|TileComplexConst.R|TileComplexConst.UL|TileComplexConst.UR|TileComplexConst.DR:
		case TileComplexConst.U|TileComplexConst.D|TileComplexConst.R|TileComplexConst.UL|TileComplexConst.DL|TileComplexConst.DR:
		case TileComplexConst.U|TileComplexConst.D|TileComplexConst.R|TileComplexConst.UR|TileComplexConst.DL|TileComplexConst.DR:
		case TileComplexConst.U|TileComplexConst.D|TileComplexConst.R|TileComplexConst.UL|TileComplexConst.UR|TileComplexConst.DL|TileComplexConst.DR:
		case TileComplexConst.U|TileComplexConst.D|TileComplexConst.L:
		case TileComplexConst.U|TileComplexConst.D|TileComplexConst.L|TileComplexConst.UL:
		case TileComplexConst.U|TileComplexConst.D|TileComplexConst.L|TileComplexConst.UR:
		case TileComplexConst.U|TileComplexConst.D|TileComplexConst.L|TileComplexConst.DL:
		case TileComplexConst.U|TileComplexConst.D|TileComplexConst.L|TileComplexConst.DR:
		case TileComplexConst.U|TileComplexConst.D|TileComplexConst.L|TileComplexConst.UL|TileComplexConst.UR:
		case TileComplexConst.U|TileComplexConst.D|TileComplexConst.L|TileComplexConst.UL|TileComplexConst.DL:
		case TileComplexConst.U|TileComplexConst.D|TileComplexConst.L|TileComplexConst.UL|TileComplexConst.DR:
		case TileComplexConst.U|TileComplexConst.D|TileComplexConst.L|TileComplexConst.UR|TileComplexConst.DL:
		case TileComplexConst.U|TileComplexConst.D|TileComplexConst.L|TileComplexConst.UR|TileComplexConst.DR:
		case TileComplexConst.U|TileComplexConst.D|TileComplexConst.L|TileComplexConst.DL|TileComplexConst.DR:
		case TileComplexConst.U|TileComplexConst.D|TileComplexConst.L|TileComplexConst.UL|TileComplexConst.UR|TileComplexConst.DL:
		case TileComplexConst.U|TileComplexConst.D|TileComplexConst.L|TileComplexConst.UL|TileComplexConst.UR|TileComplexConst.DR:
		case TileComplexConst.U|TileComplexConst.D|TileComplexConst.L|TileComplexConst.UL|TileComplexConst.DL|TileComplexConst.DR:
		case TileComplexConst.U|TileComplexConst.D|TileComplexConst.L|TileComplexConst.UR|TileComplexConst.DL|TileComplexConst.DR:
		case TileComplexConst.U|TileComplexConst.D|TileComplexConst.L|TileComplexConst.UL|TileComplexConst.UR|TileComplexConst.DL|TileComplexConst.DR:
		case TileComplexConst.UL|TileComplexConst.UR|TileComplexConst.DL|TileComplexConst.DR:
		case TileComplexConst.U|TileComplexConst.D|TileComplexConst.L|TileComplexConst.R:
		case TileComplexConst.U|TileComplexConst.D|TileComplexConst.L|TileComplexConst.R|TileComplexConst.UL:
		case TileComplexConst.U|TileComplexConst.D|TileComplexConst.L|TileComplexConst.R|TileComplexConst.UR:
		case TileComplexConst.U|TileComplexConst.D|TileComplexConst.L|TileComplexConst.R|TileComplexConst.DL:
		case TileComplexConst.U|TileComplexConst.D|TileComplexConst.L|TileComplexConst.R|TileComplexConst.DR:
		case TileComplexConst.U|TileComplexConst.D|TileComplexConst.L|TileComplexConst.R|TileComplexConst.UL|TileComplexConst.UR:
		case TileComplexConst.U|TileComplexConst.D|TileComplexConst.L|TileComplexConst.R|TileComplexConst.UL|TileComplexConst.DL:
		case TileComplexConst.U|TileComplexConst.D|TileComplexConst.L|TileComplexConst.R|TileComplexConst.UL|TileComplexConst.DR:
		case TileComplexConst.U|TileComplexConst.D|TileComplexConst.L|TileComplexConst.R|TileComplexConst.UR|TileComplexConst.DL:
		case TileComplexConst.U|TileComplexConst.D|TileComplexConst.L|TileComplexConst.R|TileComplexConst.UR|TileComplexConst.DR:
		case TileComplexConst.U|TileComplexConst.D|TileComplexConst.L|TileComplexConst.R|TileComplexConst.DL|TileComplexConst.DR:
		case TileComplexConst.U|TileComplexConst.D|TileComplexConst.L|TileComplexConst.R|TileComplexConst.UL|TileComplexConst.UR|TileComplexConst.DL:
		case TileComplexConst.U|TileComplexConst.D|TileComplexConst.L|TileComplexConst.R|TileComplexConst.UL|TileComplexConst.UR|TileComplexConst.DR:
		case TileComplexConst.U|TileComplexConst.D|TileComplexConst.L|TileComplexConst.R|TileComplexConst.UL|TileComplexConst.DL|TileComplexConst.DR:
		case TileComplexConst.U|TileComplexConst.D|TileComplexConst.L|TileComplexConst.R|TileComplexConst.UR|TileComplexConst.DL|TileComplexConst.DR:
		case TileComplexConst.U|TileComplexConst.D|TileComplexConst.L|TileComplexConst.R|TileComplexConst.UL|TileComplexConst.UR|TileComplexConst.DL|TileComplexConst.DR:
		*/

	}

	public	enum TileType
	{
		Item,
		Simple,
		Complex,
		Direction,
		Building,
		Wall,
		Normal,
		Perlin,
		Random,
		Character
	}

	public	enum TileGroup
	{
		Object,
		Terrain,
		Unit
	}

	public	enum TileFace
	{
		Up = 0,
		Down = 180,
		Left = 270,
		Right = 90
	}

	public	enum TileColor {
		Basic,
		Blue,
		Green,
		Black,
		Purple,
		Red,
		Yellow
	}

	[XmlRoot ("Config")]
	public	class TileConfig
	{
		[XmlElement ("Tutorial")]
		public	TileTutorial	tutorial;

		public	void Hashing ()
		{
			tutorial.Hashing ();
		}
	}

	public class TileLibrary
	{
		public	Dictionary<string, TileSimple>			simples;
		public	Dictionary<string, TileComplex>			complexs;
		public	Dictionary<string, TileDirection>		directions;
		public	Dictionary<string, TileBuilding>		buildings;
		public	Dictionary<string, TileWall>			walls;
		public	Dictionary<string, TileNormal>			normals;
		public	Dictionary<string, TileCharacterMotion>	motions;
		public	Dictionary<string, TileCharacter>		characters;
		public	Dictionary<string, TilePerlin>			perlins;
		public	Dictionary<string, TileRandom>			randoms;
		public	Dictionary<string, TileItem>			items;
		public	Dictionary<string, TileItemLink>		itemLinks;

		public	TileLibrary ()
		{
			simples = new Dictionary<string, TileSimple> ();
			complexs = new Dictionary<string, TileComplex> ();
			directions = new Dictionary<string, TileDirection> ();
			buildings = new Dictionary<string, TileBuilding> ();
			walls = new Dictionary<string, TileWall> ();
			normals = new Dictionary<string, TileNormal> ();
			motions = new Dictionary<string, TileCharacterMotion> ();
			characters = new Dictionary<string, TileCharacter> ();
			perlins = new Dictionary<string, TilePerlin> ();
			items = new Dictionary<string, TileItem> ();
			itemLinks = new Dictionary<string, TileItemLink> ();
		}

		public	TileItem FindItem (string id)
		{
			if (items.ContainsKey (id)) {
				return items [id];
			} else {
				return null;
			}
		}
	}

	public	class TileBase
	{
		[XmlAttribute ("id")]
		public	string	id;
		public	TileType type;
		public	TileLibrary library;

		public	virtual void Hashing() {
		}
	}

	public	class TileItem : TileBase
	{
		public	float			pivotX;
		public	float			pivotY;
		public	string			assetPath;

		public	TileItem(IDataReader dr)
		{
			this.id = dr.GetString (0);
			this.assetPath = dr.GetString (1);
			this.pivotX = dr.GetInt32 (2);
			this.pivotY = dr.GetInt32 (3);
		}
	}

	public	class TileNormal : TileBase {
		private	TileItemLink _link;
		public	TileItemLink link { get { return _link; } }

		public	TileNormal(IDataReader dr, Dictionary<string, TileItemLink> dic) {
			this.type = TileType.Normal;
			this.id = dr.GetString (0);
			this._link = dic [dr.GetString (1)];
		}
	}

	public	class TileCharacterMotion {
		private	string			_id;
		private	string			_behavior;
		private	TileItemLink	_up;
		private	TileItemLink	_down;
		private	TileItemLink	_left;
		private	TileItemLink	_right;
		public	string			id		{ get { return _id; } }
		public	string			behavior{ get { return _behavior; } }
		public	TileItemLink	up		{ get { return _up; } }
		public	TileItemLink	down	{ get { return _down; } }
		public	TileItemLink	left	{ get { return _left; } }
		public	TileItemLink	right	{ get { return _right; } }

		public	TileCharacterMotion(IDataReader dr, Dictionary<string, TileItemLink> dic) {
			this._id = dr.GetInt32 (0).ToString ();
			this._behavior = dr.GetString (1);
			this._up = dic [dr.GetString (2)];
			this._down = dic [dr.GetString (3)];
			this._left = dic [dr.GetString (4)];
			this._right = dic [dr.GetString (5)];
		}
	}

	public	class TileCharacter : TileBase {
		private	string				_name;
		private	TileCharacterMotion	_basic;
		private	TileCharacterMotion _wait;
		private	TileCharacterMotion _walk;
		private TileCharacterMotion	_attack;
		public	string				name	{ get { return _name; } }
		public	TileCharacterMotion	basic	{ get { return _basic; } }
		public	TileCharacterMotion	wait	{ get { return _wait; } }
		public	TileCharacterMotion	walk	{ get { return _walk; } }
		public	TileCharacterMotion	attack	{ get { return _attack; } }

		public	TileCharacter(IDataReader dr, Dictionary<string, TileCharacterMotion> dic) {
			this.type = TileType.Character;
			this.id = dr.GetString (0);
			this._name = dr.GetString (1);
			this._basic = dic [dr.GetString (2)];
			this._wait = dic [dr.GetString (3)];
			this._attack = dic [dr.GetString (4)];
		}
	}

	public	class TileBuilding : TileBase {
		private	TileItemLink	_basic;
		private	TileItemLink	_blue;
		private	TileItemLink	_green;
		private	TileItemLink	_black;
		private	TileItemLink	_purple;
		private	TileItemLink	_red;
		private	TileItemLink	_yellow;
		public	TileItemLink	basic	{ get { return _basic; } }
		public	TileItemLink	blue	{ get { return _blue; } }
		public	TileItemLink	green	{ get { return _green; } }
		public	TileItemLink	black	{ get { return _black; } }
		public	TileItemLink	purple	{ get { return _purple; } }
		public	TileItemLink	red		{ get { return _red; } }
		public	TileItemLink	yellow	{ get { return _yellow; } }

		public	TileBuilding(IDataReader dr, Dictionary<string, TileItemLink> dic) {
			this.type = TileType.Building;
			this.id = dr.GetString (0);
			this._basic = dic [dr.GetString (1)];
			this._blue = dic [dr.GetString (2)];
			this._green = dic [dr.GetString (3)];
			this._black = dic [dr.GetString (4)];
			this._purple = dic [dr.GetString (5)];
			this._red = dic [dr.GetString (6)];
			this._yellow = dic [dr.GetString (7)];
		}
	}

	public	class TileWall : TileBase {
		private	TileItemLink	_basic;
		private	TileItemLink	_up;
		private	TileItemLink	_left;
		private	TileItemLink	_upleft;

		public	TileItemLink	basic	{ get { return _basic; } }
		public	TileItemLink	up		{ get { return _up; } }
		public	TileItemLink	left	{ get { return _left; } }
		public	TileItemLink	upleft	{ get { return _upleft; } }

		public	TileWall (IDataReader dr, Dictionary<string, TileItemLink> dic) {
			this.type = TileType.Wall;
			this.id = dr.GetString (0);
			this._basic = dic [dr.GetString (1)];
			this._up = dic [dr.GetString (2)];
			this._upleft = dic [dr.GetString (3)];
		}
	}

	public	class TileSimple : TileBase {
		private	TileItemLink	_basic;
		private	TileItemLink	_u;
		private	TileItemLink	_d;
		private	TileItemLink	_l;
		private	TileItemLink	_r;
		private	TileItemLink	_ud;
		private	TileItemLink	_ul;
		private	TileItemLink	_ur;
		private	TileItemLink	_dl;
		private	TileItemLink	_dr;
		private	TileItemLink	_lr;
		private	TileItemLink	_udl;
		private	TileItemLink	_udr;
		private	TileItemLink	_ulr;
		private	TileItemLink	_dlr;
		private	TileItemLink	_udlr;

		public	TileItemLink	basic	{ get { return _basic; } }
		public	TileItemLink	u		{ get { return _u; } }
		public	TileItemLink	d		{ get { return _d; } }
		public	TileItemLink	l		{ get { return _l; } }
		public	TileItemLink	r		{ get { return _r; } }
		public	TileItemLink	ud		{ get { return _ud; } }
		public	TileItemLink	ul		{ get { return _ul; } }
		public	TileItemLink	ur		{ get { return _ur; } }
		public	TileItemLink	dl		{ get { return _dl; } }
		public	TileItemLink	dr		{ get { return _dr; } }
		public	TileItemLink	lr		{ get { return _lr; } }
		public	TileItemLink	udl		{ get { return _udl; } }
		public	TileItemLink	udr		{ get { return _udr; } }
		public	TileItemLink	ulr		{ get { return _ulr; } }
		public	TileItemLink	dlr		{ get { return _dlr; } }
		public	TileItemLink	udlr	{ get { return _udlr; } }

		public	TileSimple (IDataReader dr, Dictionary<string, TileItemLink> dic) {
			this.type = TileType.Simple;
			this.id = dr.GetString (0);
			this._basic = dic [dr.GetString (1)];
			this._u = dic [dr.GetString (2)];
			this._d = dic [dr.GetString (3)];
			this._l = dic [dr.GetString (4)];
			this._r = dic [dr.GetString (5)];
			this._ud = dic [dr.GetString (6)];
			this._ul = dic [dr.GetString (7)];
			this._ur = dic [dr.GetString (8)];
			this._dl = dic [dr.GetString (9)];
			this._dr = dic [dr.GetString (0)];
			this._lr = dic [dr.GetString (10)];
			this._udl = dic [dr.GetString (11)];
			this._udr = dic [dr.GetString (12)];
			this._ulr = dic [dr.GetString (13)];
			this._dlr = dic [dr.GetString (14)];
			this._udlr = dic [dr.GetString (15)];
		}
	}

	public	class TileComplex : TileBase
	{
		[XmlElement ("None")]
		public	TileItemLink	none;
		[XmlElement ("U_1C")]
		public	TileItemLink	up_1c;
		[XmlElement ("D_1C")]
		public	TileItemLink	down_1c;
		[XmlElement ("L_1C")]
		public	TileItemLink	left_1c;
		[XmlElement ("R_1C")]
		public	TileItemLink	right_1c;
		[XmlElement ("U_1S")]
		public	TileItemLink	up_1s;
		[XmlElement ("D_1S")]
		public	TileItemLink	down_1s;
		[XmlElement ("L_1S")]
		public	TileItemLink	left_1s;
		[XmlElement ("R_1S")]
		public	TileItemLink	right_1s;
		[XmlElement ("LU_1C1S")]
		public	TileItemLink	leftup_1c1s;
		[XmlElement ("DU_1C1S")]
		public	TileItemLink	downup_1c1s;
		[XmlElement ("RD_1C1S")]
		public	TileItemLink	rightdown_1c1s;
		[XmlElement ("UD_1C1S")]
		public	TileItemLink	updown_1c1s;
		[XmlElement ("RL_1C1S")]
		public	TileItemLink	rightleft_1c1s;
		[XmlElement ("DL_1C1S")]
		public	TileItemLink	downleft_1c1s;
		[XmlElement ("LR_1C1S")]
		public	TileItemLink	leftright_1c1s;
		[XmlElement ("UR_1C1S")]
		public	TileItemLink	upright_1c1s;
		[XmlElement ("DLU_2C1S")]
		public	TileItemLink	downleftup_2c1s;
		[XmlElement ("URD_2C1S")]
		public	TileItemLink	uprightdown_2c1s;
		[XmlElement ("DRL_2C1S")]
		public	TileItemLink	downrightleft_2c1s;
		[XmlElement ("ULR_2C1S")]
		public	TileItemLink	upleftright_2c1s;
		[XmlElement ("DL_2C")]
		public	TileItemLink	downleft_2c;
		[XmlElement ("UR_2C")]
		public	TileItemLink	upright_2c;
		[XmlElement ("UD_2C")]
		public	TileItemLink	updown_2c;
		[XmlElement ("LR_2C")]
		public	TileItemLink	leftright_2c;
		[XmlElement ("UL_2C")]
		public	TileItemLink	upleft_2c;
		[XmlElement ("DR_2C")]
		public	TileItemLink	downright_2c;
		[XmlElement ("UR_2S")]
		public	TileItemLink	upright_2s;
		[XmlElement ("DR_2S")]
		public	TileItemLink	downright_2s;
		[XmlElement ("DL_2S")]
		public	TileItemLink	downleft_2s;
		[XmlElement ("UL_2S")]
		public	TileItemLink	upleft_2s;
		[XmlElement ("UD_2S")]
		public	TileItemLink	updown_2s;
		[XmlElement ("LR_2S")]
		public	TileItemLink	leftright_2s;
		[XmlElement ("UDR_1C2S")]
		public	TileItemLink	updownright_1c2s;
		[XmlElement ("RDL_1C2S")]
		public	TileItemLink	rightdownleft_1c2s;
		[XmlElement ("DUL_1C2S")]
		public	TileItemLink	downupleft_1c2s;
		[XmlElement ("LUR_1C2S")]
		public	TileItemLink	leftupright_1c2s;
		[XmlElement ("DLR_3C")]
		public	TileItemLink	downleftright_3c;
		[XmlElement ("UDR_3C")]
		public	TileItemLink	updownright_3c;
		[XmlElement ("UDL_3C")]
		public	TileItemLink	updownleft_3c;
		[XmlElement ("ULR_3C")]
		public	TileItemLink	upleftright_3c;
		[XmlElement ("DLR_3S")]
		public	TileItemLink	downleftright_3s;
		[XmlElement ("ULR_3S")]
		public	TileItemLink	upleftright_3s;
		[XmlElement ("UDR_3S")]
		public	TileItemLink	updownright_3s;
		[XmlElement ("UDL_3S")]
		public	TileItemLink	updownleft_3s;
		[XmlElement ("UDLR_4C")]
		public	TileItemLink	updownleftright_4c;
		[XmlElement ("UDLR_4S")]
		public	TileItemLink	updownleftright_4s;

		public	TileComplex ()
		{
			type = TileType.Complex;
		}
	}

	public	class TilePerlin : TileBase
	{
		[XmlAttribute ("scale")]
		public	float			scale;
		[XmlElement ("Link")]
		public	TileItemLink[]	links;

		public	TilePerlin()
		{
			type = TileType.Perlin;
		}
	}

	public	class TileRandom : TileBase
	{
		[XmlElement ("Link")]
		public	TileItemLink[]	links;

		public	TileRandom()
		{
			type = TileType.Random;
		}
	}

	public	class TileDirection : TileBase
	{
		[XmlElement ("U")]
		public	TileItemLink	up;
		[XmlElement ("D")]
		public	TileItemLink	down;
		[XmlElement ("L")]
		public	TileItemLink	left;
		[XmlElement ("R")]
		public	TileItemLink	right;

		public	TileDirection ()
		{
			type = TileType.Direction;
		}
	}

	public	enum TileItemDirection
	{
		NONE = 0,
		UP = 1,
		DOWN = 2,
		LEFT = 3,
		RIGHT = 4
	}

	public	enum TileItemAniType
	{
		SINGLE = 0,
		LOOP = 1,
		MULTI = 2,
		PINGPONG = 3
	}

	public	class TileItemLink
	{
		private	string							_id;
		private TileItemDirection				_direction;
		private	TileItemAniType					_aniType;
		private	float							_fps;
		private	bool							_flipV;
		private	bool							_flipH;
		private	float							_x;
		private	float							_y;
		private	TileItem						_item;
		private	Dictionary<string, TileItem>	_items;

		public	string							id			{ get { return _id; } }
		public	TileItemDirection				direction	{ get { return _direction; } }
		public	TileItemAniType					aniType		{ get { return _aniType; } }
		public	float							fps			{ get { return _fps; } }
		public	bool							flipV		{ get { return _flipV; } }
		public	bool							flipH		{ get { return _flipH; } }
		public	float							x			{ get { return _x; } }
		public	float							y			{ get { return _y; } }
		public	Dictionary<string, TileItem>	items		{ get { return _items; } }
		public	TileItem						item		{ get { return _item; } }

		public	TileItemLink(IDataReader dr, Dictionary<string, TileItem> dic)
		{
			_id = dr.GetInt32 (0).ToString();
			_direction = (TileItemDirection) dr.GetInt32 (1);
			_aniType = (TileItemAniType) dr.GetInt32 (2);
			_fps = dr.GetInt32 (3);
			_flipV = dr.GetInt32 (4) == 1;
			_flipH = dr.GetInt32 (5) == 1;
			_x = (float) dr.GetInt32 (6);
			_y = (float) dr.GetInt32 (7);

			var list = new List<string> ();
			var ptr = 8;
			while (!dr.IsDBNull (ptr))
			{
				list.Add (dr.GetString (ptr));
				ptr++;
				if (ptr > 15)
					break;
			}

			_items = new Dictionary<string, TileItem>(list.Count);
			int total = 0;
			for (int i = 0; i < total; i++) {
				var current = list [i];
				var tile = dic [current];
				if (_item == null) {
					_item = tile;
				}
				if (dic.ContainsKey (current)) {
					_items.Add (current, tile);
				} else {
					throw new KeyNotFoundException (current);
				}
			}
		}
	}

	public	class TileTutorial
	{
		[XmlElement ("Map")]
		public	TileMap[]	maps;
		private	Dictionary<string, TileMap>	_dictionaryMap;

		public	TileTutorial ()
		{
			maps = new TileMap[0];
			_dictionaryMap = new Dictionary<string, TileMap> ();
		}

		public	void Hashing ()
		{
			_dictionaryMap.Clear ();
			for (int i = maps.Length - 1; i >= 0; i--) {
				TileMap map = maps [i];
				map.Hashing ();
				_dictionaryMap.Add (map.id, map);
			}
		}

		public	TileMap FindItem (string id)
		{
			if (_dictionaryMap.ContainsKey (id)) {
				return _dictionaryMap [id];
			} else {
				return null;
			}
		}
	}

	public	class TileMap
	{
		[XmlAttribute ("id")]
		public	string	id;
		[XmlElement ("Terrain")]
		public	TileTerrain[]	terrains;
		[XmlElement ("Unit")]
		public	TileUnit[]	units;
		[XmlAttribute ("width")]
		public	int width;
		[XmlAttribute ("height")]
		public	int height;
		[XmlAttribute ("depth")]
		public	int depth;
		private List<List<List<TileObject>>> _tlist;

		public	TileMap ()
		{
			this.id = string.Empty;
			this.terrains = new TileTerrain[0];
			this.units = new TileUnit[0];
			this.width = 1;
			this.height = 1;
			this.depth = 1;
		}

		public	TileMap (int w, int h, int d)
		{
			this.id = string.Empty;
			this.terrains = new TileTerrain[0];
			this.units = new TileUnit[0];
			this.width = w;
			this.height = h;
			this.depth = d;
		}

		public	TileMap Clone ()
		{
			var c = new TileMap ();
			c.id = this.id;
			c.width = this.width;
			c.height = this.height;
			c.depth = this.depth;

			if (this.terrains != null) {
				var len = this.terrains.Length;
				c.terrains = new TileTerrain[len];
				System.Array.Copy (this.terrains, c.terrains, len);
			}
			if (this.units != null) {
				var len = this.units.Length;
				c.units = new TileUnit[len];
				System.Array.Copy (this.units, c.units, len);
			}

			return c;
		}

		public	void Hashing ()
		{
			var wlist = new List<List<List<TileObject>>> (width);
			for (int i = 0; i < width; i++) {
				var hlist = new List<List<TileObject>> (height);
				wlist.Add (hlist);
				for (int j = 0; j < height; j++) {
					var dlist = new List<TileObject> (depth);
					hlist.Add (dlist);
					for (int k = 0; k < depth; k++) {
						dlist.Add (null);
					}
				}
			}

			_tlist = wlist;

			if (terrains == null) {
				terrains = new TileTerrain[0];
			}
			if (units == null) {
				units = new TileUnit[0];
			}

			for (int i = terrains.Length - 1; i >= 0; i--) {
				var terrain = terrains [i];
				_tlist [terrain.x] [terrain.y] [terrain.z] = terrain;
				terrain.SetMap (this);
			}
			for (int i = units.Length - 1; i >= 0; i--) {
				var unit = units [i];
				_tlist [unit.x] [unit.y] [unit.z] = unit;
				unit.SetMap (this);
			}
		}

		public	bool AddTileObject(TileObject obj)
		{
			if (_tlist [obj.x] [obj.y] [obj.z] == null) {
				_tlist [obj.x] [obj.y] [obj.z] = obj;
				obj.SetMap (this);

				switch (obj.group) {
				case TileGroup.Terrain:
					System.Array.Resize<TileTerrain> (ref terrains, terrains.Length + 1);
					terrains [terrains.Length - 1] = obj as TileTerrain;
					break;
				case TileGroup.Unit:
					System.Array.Resize<TileUnit> (ref units, units.Length + 1);
					units [units.Length - 1] = obj as TileUnit;
					break;
				}
				return true;
			} else {
				Debug.LogWarningFormat ("Conflict x:{0}, y:{1}, z:{2}", obj.x, obj.y, obj.z);
				return false;
			}
		}

		public	bool RemoveTileObject (TileObject obj)
		{
			if (_tlist [obj.x] [obj.y] [obj.z] == obj) {
				_tlist [obj.x] [obj.y] [obj.z] = null;
				obj.SetMap (null);

				switch (obj.group) {
				case TileGroup.Terrain:
					terrains = System.Array.FindAll<TileTerrain> (terrains, (TileTerrain a) => {
						return a != obj;
					});
					break;
				case TileGroup.Unit:
					units = System.Array.FindAll<TileUnit> (units, (TileUnit a) => {
						return a != obj;
					});
					break;
				}
				return true;
			}
			return false;
		}

		public	TileObject GetTileObject (int x, int y, int z)
		{
			if (x < 0 || x >= width || y < 0 || y >= height || z < 0 || z >= depth) {
				return null;
			} else {
				return _tlist [x] [y] [z];
			}
		}
	}

	public	class TileObject {
		private	static int sequence = 0;
		[XmlAttribute ("item")]
		public		string		item;
		[XmlAttribute ("x")]
		public		float		xf;
		[XmlAttribute ("y")]
		public		float 		yf;
		[XmlAttribute ("z")]
		public		float 		zf;
		[XmlAttribute ("face")]
		public		TileFace	face;
		public		TileGroup	group;
		protected	TileMap		map;
		private		float 		_rnd;
		private		int 		_seq;

		public	TileObject ()
		{
			this.item = string.Empty;
			this.face = TileFace.Up;
			this.group = TileGroup.Object;
			this.map = null;
			this._rnd = Random.value;
			this._seq = sequence++;
			SetPosition (0f, 0f, 0f);
		}

		public	TileObject (string item, float x, float y, float z)
		{
			this.item = item;
			this.face = TileFace.Up;
			this.group = TileGroup.Object;
			this.map = null;
			this._rnd = Random.value;
			this._seq = sequence++;
			SetPosition (x, y, z);
		}

		public	void SetPosition (float x, float y, float z)
		{
			this.xf = x;
			this.yf = y;
			this.zf = z;
		}

		public	void SetPosition (Vector3 position)
		{
			SetPosition (position.x, position.y, position.z);
		}

		public	int x {
			get {
				return (int)xf;
			}
		}

		public	int y {
			get {
				return (int)yf;
			}
		}

		public	int z {
			get {
				return (int)zf;
			}
		}

		public	float valueRandom {
			get {
				return _rnd;
			}
		}

		public	int valueSequence {
			get {
				return _seq;
			}
		}

		public	void SetMap (TileMap map) {
			this.map = map;
		}

		public	TileMap GetMap () {
			return map;
		}

		protected	void GetAllTileItem (TileItemLink link, List<TileItemLink> list)
		{
			if (!string.IsNullOrEmpty (link.item)) {
				list.Add (link);
			}
			int total = link.children != null ? link.children.Length : 0;
			for (int i = 0; i < total; i++) {
				GetAllTileItem (link.children [i], list);
			}
		}
	}

	public	interface ITileObject {
		List<TileItemLink> GetTileItemLink (TileLibrary library);
	}

	public	class TileUnit : TileObject, ITileObject {
		[XmlAttribute ("color")]
		public	TileColor	color;

		public	TileUnit () : base () {
			this.group = TileGroup.Unit;
		}

		public	TileUnit (string item, float x, float y, float z) : base (item, x, y, z) {
			this.color = TileColor.Basic;
		}

		public	TileUnit u {
			get {
				if (map != null) {
					return map.GetTileObject (x, y, z - 1) as TileUnit;
				} else {
					return null;
				}
			}
		}

		public	TileUnit l {
			get {
				if (map != null) {
					return map.GetTileObject (x - 1, y, z) as TileUnit;
				} else {
					return null;
				}
			}
		}

		public	TileUnit ul {
			get {
				if (map != null) {
					return map.GetTileObject (x - 1, y, z - 1) as TileUnit;
				} else {
					return null;
				}
			}
		}

		public List<TileItemLink> GetTileItemLink (TileLibrary library) {
			TileBase tb = library.FindItem (item);

			List<TileItemLink> list = new List<TileItemLink> (8);
			switch (tb.type) {
			case TileType.Normal:
				list.Add ((tb as TileNormal).link);
				break;
			case TileType.Building:
				GetTileItemForBuilding (tb as TileBuilding, list);
				break;
			case TileType.Wall:
				GetTileItemForWall (library, tb as TileWall, list);
				break;
			case TileType.Character:
				GetTileItemForCharacter (tb as TileCharacter, list);
				break;
			}

			return list;
		}

		private	void GetTileItemForBuilding (TileBuilding building, List<TileItemLink> list) {
			switch (color) {
			case TileColor.Basic:
				GetAllTileItem (building.basic, list);
				break;
			case TileColor.Blue:
				GetAllTileItem (building.blue, list);
				break;
			case TileColor.Green:
				GetAllTileItem (building.green, list);
				break;
			case TileColor.Black:
				GetAllTileItem (building.black, list);
				break;
			case TileColor.Purple:
				GetAllTileItem (building.purple, list);
				break;
			case TileColor.Red:
				GetAllTileItem (building.red, list);
				break;
			case TileColor.Yellow:
				GetAllTileItem (building.yellow, list);
				break;
			}
		}

		private	void GetTileItemForWall (TileLibrary library, TileWall wall, List<TileItemLink> list)
		{
			var up = u;
			var left = l;
			uint result = 0;
			if (up != null && TileType.Wall == library.FindItem (up.item).type) {
				result |= TileWallConst.U;
			}
			if (left != null && TileType.Wall == library.FindItem (left.item).type) {
				result |= TileWallConst.L;
			}

			switch (result) {
			case TileWallConst.U:
				GetAllTileItem (wall.up, list);
				break;
			case TileWallConst.L:
				GetAllTileItem (wall.left, list);
				break;
			case TileWallConst.U_L:
				GetAllTileItem (wall.upleft, list);
				break;
			default:
				GetAllTileItem (wall.none, list);
				break;
			}
		}

		private	void GetTileItemForCharacter (TileCharacter character, List<TileItemLink> list) {
			switch (face) {
			case TileFace.Up:
				GetAllTileItem (character.GetMotion ().up, list);
				break;
			case TileFace.Down:
				GetAllTileItem (character.GetMotion ().down, list);
				break;
			case TileFace.Left:
				GetAllTileItem (character.GetMotion ().left, list);
				break;
			case TileFace.Right:
				GetAllTileItem (character.GetMotion ().right, list);
				break;
			}
		}
	}

	public	class TileTerrain : TileObject, ITileObject
	{
		public	TileTerrain () : base () {
			this.group = TileGroup.Terrain;
		}

		public	TileTerrain (string item, float x, float y, float z) : base (item, x, y, z) {
			this.group = TileGroup.Terrain;
		}

		public	TileTerrain u {
			get {
				if (map != null) {
					return map.GetTileObject (x, y, z - 1) as TileTerrain;
				} else {
					return null;
				}
			}
		}

		public	TileTerrain d {
			get {
				if (map != null) {
					return map.GetTileObject (x, y, z + 1) as TileTerrain;
				} else {
					return null;
				}
			}
		}

		public	TileTerrain l {
			get {
				if (map != null) {
					return map.GetTileObject (x - 1, y, z) as TileTerrain;
				} else {
					return null;
				}
			}
		}

		public	TileTerrain r {
			get {
				if (map != null) {
					return map.GetTileObject (x + 1, y, z) as TileTerrain;
				} else {
					return null;
				}
			}
		}

		public	TileTerrain ul {
			get {
				if (map != null) {
					return map.GetTileObject (x - 1, y, z - 1) as TileTerrain;
				} else {
					return null;
				}
			}
		}

		public	TileTerrain ur {
			get {
				if (map != null) {
					return map.GetTileObject (x + 1, y, z - 1) as TileTerrain;
				} else {
					return null;
				}
			}
		}

		public	TileTerrain dl {
			get {
				if (map != null) {
					return map.GetTileObject (x - 1, y, z + 1) as TileTerrain;
				} else {
					return null;
				}
			}
		}

		public	TileTerrain dr {
			get {
				if (map != null) {
					return map.GetTileObject (x + 1, y, z + 1) as TileTerrain;
				} else {
					return null;
				}
			}
		}

		public List<TileItemLink> GetTileItemLink (TileLibrary library)
		{
			TileBase tb = library.FindItem (item);

			List<TileItemLink> list = new List<TileItemLink> (8);
			switch (tb.type) {
			case TileType.Normal:
				list.Add ((tb as TileNormal).link);
				break;
			case TileType.Simple:
				GetTileItemForSimple (library, tb as TileSimple, list);
				break;
			case TileType.Complex:
				GetTileItemForComplex (library, tb as TileComplex, list);
				break;
			case TileType.Direction:
				GetTileItemForDirection (library, tb as TileDirection, list);
				break;
			case TileType.Perlin:
				GetTileItemForPerlin (library, tb as TilePerlin, list);
				break;
			case TileType.Random:
				GetTileItemForRandom (library, tb as TileRandom, list);
				break;
			}

			return list;
		}

		private	void GetTileItemForSimple (TileLibrary library, TileSimple simple, List<TileItemLink> list)
		{
			var up = u;
			var down = d;
			var left = l;
			var right = r;
			uint result = 0;
			string tbid = simple.id;

			if (up != null && tbid == library.FindItem (up.item).id) {
				result |= TileSimpleConst.U;
			}
			if (down != null && tbid == library.FindItem (down.item).id) {
				result |= TileSimpleConst.D;
			}
			if (left != null && tbid == library.FindItem (left.item).id) {
				result |= TileSimpleConst.L;
			}
			if (right != null && tbid == library.FindItem (right.item).id) {
				result |= TileSimpleConst.R;
			}

			switch (result) {
			case TileSimpleConst.U:
				GetAllTileItem (simple.up, list);
				break;
			case TileSimpleConst.D:
				GetAllTileItem (simple.down, list);
				break;
			case TileSimpleConst.L:
				GetAllTileItem (simple.left, list);
				break;
			case TileSimpleConst.R:
				GetAllTileItem (simple.right, list);
				break;
			case TileSimpleConst.U_D:
				GetAllTileItem (simple.updown, list);
				break;
			case TileSimpleConst.U_L:
				GetAllTileItem (simple.upleft, list);
				break;
			case TileSimpleConst.U_R:
				GetAllTileItem (simple.upright, list);
				break;
			case TileSimpleConst.D_L:
				GetAllTileItem (simple.downleft, list);
				break;
			case TileSimpleConst.D_R:
				GetAllTileItem (simple.downright, list);
				break;
			case TileSimpleConst.L_R:
				GetAllTileItem (simple.leftright, list);
				break;
			case TileSimpleConst.U_D_L:
				GetAllTileItem (simple.updownleft, list);
				break;
			case TileSimpleConst.U_D_R:
				GetAllTileItem (simple.updownright, list);
				break;
			case TileSimpleConst.U_L_R:
				GetAllTileItem (simple.upleftright, list);
				break;
			case TileSimpleConst.D_L_R:
				GetAllTileItem (simple.downleftright, list);
				break;
			case TileSimpleConst.U_D_L_R:
				GetAllTileItem (simple.updownleftright, list);
				break;
			default:
				GetAllTileItem (simple.none, list);
				break;
			}
		}

		private	void GetTileItemForComplex (TileLibrary library, TileComplex complex, List<TileItemLink> list)
		{
			var up = u;
			var down = d;
			var left = l;
			var right = r;
			var north = ul;
			var south = dr;
			var east = ur;
			var west = dl;
			uint result = 0;
			string tbid = complex.id;

			if (up != null && tbid != library.FindItem (up.item).id) {
				result |= TileComplexConst.U;
			}
			if (down != null && tbid != library.FindItem (down.item).id) {
				result |= TileComplexConst.D;
			}
			if (left != null && tbid != library.FindItem (left.item).id) {
				result |= TileComplexConst.L;
			}
			if (right != null && tbid != library.FindItem (right.item).id) {
				result |= TileComplexConst.R;
			}
			if (north != null && tbid != library.FindItem (north.item).id) {
				result |= TileComplexConst.UL;
			}
			if (south != null && tbid != library.FindItem (south.item).id) {
				result |= TileComplexConst.DR;
			}
			if (east != null && tbid != library.FindItem (east.item).id) {
				result |= TileComplexConst.UR;
			}
			if (west != null && tbid != library.FindItem (west.item).id) {
				result |= TileComplexConst.DL;
			}

			switch (result) {
			case TileComplexConst.NONE:
				GetAllTileItem (complex.none, list);
				return;
			case TileComplexConst.UL:
				GetAllTileItem (complex.up_1c, list);
				return;
			case TileComplexConst.DR:
				GetAllTileItem (complex.down_1c, list);
				return;
			case TileComplexConst.DL:
				GetAllTileItem (complex.left_1c, list);
				return;
			case TileComplexConst.UR:
				GetAllTileItem (complex.right_1c, list);
				return;
			case TileComplexConst.U:
			case TileComplexConst.U_UL:
			case TileComplexConst.U_UR:
			case TileComplexConst.U_UL_UR:	
				GetAllTileItem (complex.up_1s, list);
				return;
			case TileComplexConst.D:
			case TileComplexConst.D_DL:
			case TileComplexConst.D_DR:
			case TileComplexConst.D_DL_DR:
				GetAllTileItem (complex.down_1s, list);
				return;
			case TileComplexConst.L:
			case TileComplexConst.L_UL:
			case TileComplexConst.L_DL:
			case TileComplexConst.L_UL_DL:
				GetAllTileItem (complex.left_1s, list);
				return;
			case TileComplexConst.R:
			case TileComplexConst.R_UR:
			case TileComplexConst.R_DR:
			case TileComplexConst.R_UR_DR:
				GetAllTileItem (complex.right_1s, list);
				return;
			case TileComplexConst.U_DL:
			case TileComplexConst.U_DL_UL:
			case TileComplexConst.U_DL_UR:
			case TileComplexConst.U_DL_UL_UR:
				GetAllTileItem (complex.leftup_1c1s, list);
				return;
			case TileComplexConst.U_DR:
			case TileComplexConst.U_DR_UL:
			case TileComplexConst.U_DR_UR:
			case TileComplexConst.U_DR_UL_UR:
				GetAllTileItem (complex.downup_1c1s, list);
				return;
			case TileComplexConst.D_UR:
			case TileComplexConst.D_UR_DL:
			case TileComplexConst.D_UR_DR:
			case TileComplexConst.D_UR_DL_DR:
				GetAllTileItem (complex.rightdown_1c1s, list);
				return;
			case TileComplexConst.D_UL:
			case TileComplexConst.D_UL_DL:
			case TileComplexConst.D_UL_DR:
			case TileComplexConst.D_UL_DL_DR:
				GetAllTileItem (complex.updown_1c1s, list);
				return;
			case TileComplexConst.L_UR:
			case TileComplexConst.L_UR_UL:
			case TileComplexConst.L_UR_DL:
			case TileComplexConst.L_UR_UL_DL:
				GetAllTileItem (complex.rightleft_1c1s, list);
				return;
			case TileComplexConst.L_DR:
			case TileComplexConst.L_DR_UL:
			case TileComplexConst.L_DR_DL:
			case TileComplexConst.L_DR_UL_DL:
				GetAllTileItem (complex.downleft_1c1s, list);
				return;
			case TileComplexConst.R_DL:
			case TileComplexConst.R_DL_UR:
			case TileComplexConst.R_DL_DR:
			case TileComplexConst.R_DL_UR_DR:
				GetAllTileItem (complex.leftright_1c1s, list);
				return;
			case TileComplexConst.R_UL:
			case TileComplexConst.R_UL_UR:
			case TileComplexConst.R_UL_DR:
			case TileComplexConst.R_UL_UR_DR:
				GetAllTileItem (complex.upright_1c1s, list);
				return;
			case TileComplexConst.U_DL_DR:
			case TileComplexConst.U_DL_DR_UL:
			case TileComplexConst.U_DL_DR_UR:
			case TileComplexConst.U_DL_DR_UL_UR:
				GetAllTileItem (complex.downleftup_2c1s, list);
				return;
			case TileComplexConst.D_UL_UR:
			case TileComplexConst.D_UL_UR_DL:
			case TileComplexConst.D_UL_UR_DR:
			case TileComplexConst.D_UL_UR_DL_DR:
				GetAllTileItem (complex.uprightdown_2c1s, list);
				return;
			case TileComplexConst.L_UR_DR:
			case TileComplexConst.L_UR_DR_UL:
			case TileComplexConst.L_UR_DR_DL:
			case TileComplexConst.L_UR_DR_UL_DL:
				GetAllTileItem (complex.downrightleft_2c1s, list);
				return;
			case TileComplexConst.R_UL_DL:
			case TileComplexConst.R_UL_DL_UR:
			case TileComplexConst.R_UL_DL_DR:
			case TileComplexConst.R_UL_DL_UR_DR:
				GetAllTileItem (complex.upleftright_2c1s, list);
				return;
			case TileComplexConst.DL_DR:
				GetAllTileItem (complex.downleft_2c, list);
				return;
			case TileComplexConst.UL_UR:
				GetAllTileItem (complex.upright_2c, list);
				return;
			case TileComplexConst.UL_DR:
				GetAllTileItem (complex.updown_2c, list);
				return;
			case TileComplexConst.DL_UR:
				GetAllTileItem (complex.leftright_2c, list);
				return;
			case TileComplexConst.UL_DL:
				GetAllTileItem (complex.upleft_2c, list);
				return;
			case TileComplexConst.UR_DR:
				GetAllTileItem (complex.downright_2c, list);
				return;
			case TileComplexConst.U_R:
			case TileComplexConst.U_R_UL:
			case TileComplexConst.U_R_UR:
			case TileComplexConst.U_R_DR:
			case TileComplexConst.U_R_UL_UR:
			case TileComplexConst.U_R_UL_DR:
			case TileComplexConst.U_R_UR_DR:
			case TileComplexConst.U_R_UL_UR_DR:
				GetAllTileItem (complex.upright_2s, list);
				return;
			case TileComplexConst.D_R:
			case TileComplexConst.D_R_DL:
			case TileComplexConst.D_R_UR:
			case TileComplexConst.D_R_DR:
			case TileComplexConst.D_R_DL_UR:
			case TileComplexConst.D_R_DL_DR:
			case TileComplexConst.D_R_UR_DR:
			case TileComplexConst.D_R_DL_UR_DR:
				GetAllTileItem (complex.downright_2s, list);
				return;
			case TileComplexConst.D_L:
			case TileComplexConst.D_L_DL:
			case TileComplexConst.D_L_UL:
			case TileComplexConst.D_L_DR:
			case TileComplexConst.D_L_DL_UL:
			case TileComplexConst.D_L_DL_DR:
			case TileComplexConst.D_L_UL_DR:
			case TileComplexConst.D_L_DL_UL_DR:
				GetAllTileItem (complex.downleft_2s, list);
				return;
			case TileComplexConst.U_L:
			case TileComplexConst.U_L_DL:
			case TileComplexConst.U_L_UL:
			case TileComplexConst.U_L_UR:
			case TileComplexConst.U_L_DL_UL:
			case TileComplexConst.U_L_DL_UR:
			case TileComplexConst.U_L_UL_UR:
			case TileComplexConst.U_L_DL_UL_UR:
				GetAllTileItem (complex.upleft_2s, list);
				return;
			case TileComplexConst.U_D:
			case TileComplexConst.U_D_UL:
			case TileComplexConst.U_D_UR:
			case TileComplexConst.U_D_DL:
			case TileComplexConst.U_D_DR:
			case TileComplexConst.U_D_UL_UR:
			case TileComplexConst.U_D_UL_DL:
			case TileComplexConst.U_D_UL_DR:
			case TileComplexConst.U_D_UR_DL:
			case TileComplexConst.U_D_UR_DR:
			case TileComplexConst.U_D_DL_DR:
			case TileComplexConst.U_D_UL_UR_DL:
			case TileComplexConst.U_D_UL_UR_DR:
			case TileComplexConst.U_D_UL_DL_DR:
			case TileComplexConst.U_D_UR_DL_DR:
			case TileComplexConst.U_D_UL_UR_DL_DR:
				GetAllTileItem (complex.updown_2s, list);
				return;
			case TileComplexConst.L_R:
			case TileComplexConst.L_R_UL:
			case TileComplexConst.L_R_UR:
			case TileComplexConst.L_R_DL:
			case TileComplexConst.L_R_DR:
			case TileComplexConst.L_R_UL_UR:
			case TileComplexConst.L_R_UL_DL:
			case TileComplexConst.L_R_UL_DR:
			case TileComplexConst.L_R_UR_DL:
			case TileComplexConst.L_R_UR_DR:
			case TileComplexConst.L_R_DL_DR:
			case TileComplexConst.L_R_UL_UR_DL:
			case TileComplexConst.L_R_UL_UR_DR:
			case TileComplexConst.L_R_UL_DL_DR:
			case TileComplexConst.L_R_UR_DL_DR:
			case TileComplexConst.L_R_UL_UR_DL_DR:
				GetAllTileItem (complex.leftright_2s, list);
				return;
			case TileComplexConst.D_R_UL:
			case TileComplexConst.D_R_UL_UR:
			case TileComplexConst.D_R_UL_DL:
			case TileComplexConst.D_R_UL_DR:
			case TileComplexConst.D_R_UL_UR_DL:
			case TileComplexConst.D_R_UL_UR_DR:
			case TileComplexConst.D_R_UL_DL_DR:
			case TileComplexConst.D_R_UL_UR_DL_DR:
				GetAllTileItem (complex.updownright_1c2s, list);
				return;
			case TileComplexConst.D_L_UR:
			case TileComplexConst.D_L_UR_UL:
			case TileComplexConst.D_L_UR_DL:
			case TileComplexConst.D_L_UR_DR:
			case TileComplexConst.D_L_UR_UL_DL:
			case TileComplexConst.D_L_UR_UL_DR:
			case TileComplexConst.D_L_UR_DL_DR:
			case TileComplexConst.D_L_UR_UL_DL_DR:
				GetAllTileItem (complex.rightdownleft_1c2s, list);
				return;
			case TileComplexConst.U_L_DR:
			case TileComplexConst.U_L_DR_UL:
			case TileComplexConst.U_L_DR_DL:
			case TileComplexConst.U_L_DR_UR:
			case TileComplexConst.U_L_DR_UL_DL:
			case TileComplexConst.U_L_DR_UL_UR:
			case TileComplexConst.U_L_DR_DL_UR:
			case TileComplexConst.U_L_DR_UL_DL_UR:
				GetAllTileItem (complex.downupleft_1c2s, list);
				return;
			case TileComplexConst.U_R_DL:
			case TileComplexConst.U_R_DL_UL:
			case TileComplexConst.U_R_DL_DR:
			case TileComplexConst.U_R_DL_UR:
			case TileComplexConst.U_R_DL_UL_DR:
			case TileComplexConst.U_R_DL_UL_UR:
			case TileComplexConst.U_R_DL_DR_UR:
			case TileComplexConst.U_R_DL_UL_DR_UR:
				GetAllTileItem (complex.leftupright_1c2s, list);
				return;
			case TileComplexConst.UR|TileComplexConst.DL|TileComplexConst.DR:
				GetAllTileItem (complex.downleftright_3c, list);
				return;
			case TileComplexConst.UL|TileComplexConst.UR|TileComplexConst.DR:
				GetAllTileItem (complex.updownright_3c, list);
				return;
			case TileComplexConst.UL|TileComplexConst.DL|TileComplexConst.DR:
				GetAllTileItem (complex.updownleft_3c, list);
				return;
			case TileComplexConst.UL|TileComplexConst.UR|TileComplexConst.DL:
				GetAllTileItem (complex.upleftright_3c, list);
				return;
			case TileComplexConst.D_L_R:
			case TileComplexConst.D_L_R_UL:
			case TileComplexConst.D_L_R_UR:
			case TileComplexConst.D_L_R_DL:
			case TileComplexConst.D_L_R_DR:
			case TileComplexConst.D_L_R_UL_UR:
			case TileComplexConst.D_L_R_UL_DL:
			case TileComplexConst.D_L_R_UL_DR:
			case TileComplexConst.D_L_R_UR_DL:
			case TileComplexConst.D_L_R_UR_DR:
			case TileComplexConst.D_L_R_DL_DR:
			case TileComplexConst.D_L_R_UL_UR_DL:
			case TileComplexConst.D_L_R_UL_UR_DR:
			case TileComplexConst.D_L_R_UL_DL_DR:
			case TileComplexConst.D_L_R_UR_DL_DR:
			case TileComplexConst.D_L_R_UL_UR_DL_DR:
				GetAllTileItem (complex.downleftright_3s, list);
				return;
			case TileComplexConst.U|TileComplexConst.L|TileComplexConst.R:
			case TileComplexConst.U|TileComplexConst.L|TileComplexConst.R|TileComplexConst.UL:
			case TileComplexConst.U|TileComplexConst.L|TileComplexConst.R|TileComplexConst.UR:
			case TileComplexConst.U|TileComplexConst.L|TileComplexConst.R|TileComplexConst.DL:
			case TileComplexConst.U|TileComplexConst.L|TileComplexConst.R|TileComplexConst.DR:
			case TileComplexConst.U|TileComplexConst.L|TileComplexConst.R|TileComplexConst.UL|TileComplexConst.UR:
			case TileComplexConst.U|TileComplexConst.L|TileComplexConst.R|TileComplexConst.UL|TileComplexConst.DL:
			case TileComplexConst.U|TileComplexConst.L|TileComplexConst.R|TileComplexConst.UL|TileComplexConst.DR:
			case TileComplexConst.U|TileComplexConst.L|TileComplexConst.R|TileComplexConst.UR|TileComplexConst.DL:
			case TileComplexConst.U|TileComplexConst.L|TileComplexConst.R|TileComplexConst.UR|TileComplexConst.DR:
			case TileComplexConst.U|TileComplexConst.L|TileComplexConst.R|TileComplexConst.DL|TileComplexConst.DR:
			case TileComplexConst.U|TileComplexConst.L|TileComplexConst.R|TileComplexConst.UL|TileComplexConst.UR|TileComplexConst.DL:
			case TileComplexConst.U|TileComplexConst.L|TileComplexConst.R|TileComplexConst.UL|TileComplexConst.UR|TileComplexConst.DR:
			case TileComplexConst.U|TileComplexConst.L|TileComplexConst.R|TileComplexConst.UL|TileComplexConst.DL|TileComplexConst.DR:
			case TileComplexConst.U|TileComplexConst.L|TileComplexConst.R|TileComplexConst.UR|TileComplexConst.DL|TileComplexConst.DR:
			case TileComplexConst.U|TileComplexConst.L|TileComplexConst.R|TileComplexConst.UL|TileComplexConst.UR|TileComplexConst.DL|TileComplexConst.DR:
				GetAllTileItem (complex.upleftright_3s, list);
				return;
			case TileComplexConst.U|TileComplexConst.D|TileComplexConst.R:
			case TileComplexConst.U|TileComplexConst.D|TileComplexConst.R|TileComplexConst.UL:
			case TileComplexConst.U|TileComplexConst.D|TileComplexConst.R|TileComplexConst.UR:
			case TileComplexConst.U|TileComplexConst.D|TileComplexConst.R|TileComplexConst.DL:
			case TileComplexConst.U|TileComplexConst.D|TileComplexConst.R|TileComplexConst.DR:
			case TileComplexConst.U|TileComplexConst.D|TileComplexConst.R|TileComplexConst.UL|TileComplexConst.UR:
			case TileComplexConst.U|TileComplexConst.D|TileComplexConst.R|TileComplexConst.UL|TileComplexConst.DL:
			case TileComplexConst.U|TileComplexConst.D|TileComplexConst.R|TileComplexConst.UL|TileComplexConst.DR:
			case TileComplexConst.U|TileComplexConst.D|TileComplexConst.R|TileComplexConst.UR|TileComplexConst.DL:
			case TileComplexConst.U|TileComplexConst.D|TileComplexConst.R|TileComplexConst.UR|TileComplexConst.DR:
			case TileComplexConst.U|TileComplexConst.D|TileComplexConst.R|TileComplexConst.DL|TileComplexConst.DR:
			case TileComplexConst.U|TileComplexConst.D|TileComplexConst.R|TileComplexConst.UL|TileComplexConst.UR|TileComplexConst.DL:
			case TileComplexConst.U|TileComplexConst.D|TileComplexConst.R|TileComplexConst.UL|TileComplexConst.UR|TileComplexConst.DR:
			case TileComplexConst.U|TileComplexConst.D|TileComplexConst.R|TileComplexConst.UL|TileComplexConst.DL|TileComplexConst.DR:
			case TileComplexConst.U|TileComplexConst.D|TileComplexConst.R|TileComplexConst.UR|TileComplexConst.DL|TileComplexConst.DR:
			case TileComplexConst.U|TileComplexConst.D|TileComplexConst.R|TileComplexConst.UL|TileComplexConst.UR|TileComplexConst.DL|TileComplexConst.DR:
				GetAllTileItem (complex.updownright_3s, list);
				return;
			case TileComplexConst.U|TileComplexConst.D|TileComplexConst.L:
			case TileComplexConst.U|TileComplexConst.D|TileComplexConst.L|TileComplexConst.UL:
			case TileComplexConst.U|TileComplexConst.D|TileComplexConst.L|TileComplexConst.UR:
			case TileComplexConst.U|TileComplexConst.D|TileComplexConst.L|TileComplexConst.DL:
			case TileComplexConst.U|TileComplexConst.D|TileComplexConst.L|TileComplexConst.DR:
			case TileComplexConst.U|TileComplexConst.D|TileComplexConst.L|TileComplexConst.UL|TileComplexConst.UR:
			case TileComplexConst.U|TileComplexConst.D|TileComplexConst.L|TileComplexConst.UL|TileComplexConst.DL:
			case TileComplexConst.U|TileComplexConst.D|TileComplexConst.L|TileComplexConst.UL|TileComplexConst.DR:
			case TileComplexConst.U|TileComplexConst.D|TileComplexConst.L|TileComplexConst.UR|TileComplexConst.DL:
			case TileComplexConst.U|TileComplexConst.D|TileComplexConst.L|TileComplexConst.UR|TileComplexConst.DR:
			case TileComplexConst.U|TileComplexConst.D|TileComplexConst.L|TileComplexConst.DL|TileComplexConst.DR:
			case TileComplexConst.U|TileComplexConst.D|TileComplexConst.L|TileComplexConst.UL|TileComplexConst.UR|TileComplexConst.DL:
			case TileComplexConst.U|TileComplexConst.D|TileComplexConst.L|TileComplexConst.UL|TileComplexConst.UR|TileComplexConst.DR:
			case TileComplexConst.U|TileComplexConst.D|TileComplexConst.L|TileComplexConst.UL|TileComplexConst.DL|TileComplexConst.DR:
			case TileComplexConst.U|TileComplexConst.D|TileComplexConst.L|TileComplexConst.UR|TileComplexConst.DL|TileComplexConst.DR:
			case TileComplexConst.U|TileComplexConst.D|TileComplexConst.L|TileComplexConst.UL|TileComplexConst.UR|TileComplexConst.DL|TileComplexConst.DR:
				GetAllTileItem (complex.updownleft_3s, list);
				return;
			case TileComplexConst.UL|TileComplexConst.UR|TileComplexConst.DL|TileComplexConst.DR:
				GetAllTileItem (complex.updownleftright_4c, list);
				return;
			case TileComplexConst.U|TileComplexConst.D|TileComplexConst.L|TileComplexConst.R:
			case TileComplexConst.U|TileComplexConst.D|TileComplexConst.L|TileComplexConst.R|TileComplexConst.UL:
			case TileComplexConst.U|TileComplexConst.D|TileComplexConst.L|TileComplexConst.R|TileComplexConst.UR:
			case TileComplexConst.U|TileComplexConst.D|TileComplexConst.L|TileComplexConst.R|TileComplexConst.DL:
			case TileComplexConst.U|TileComplexConst.D|TileComplexConst.L|TileComplexConst.R|TileComplexConst.DR:
			case TileComplexConst.U|TileComplexConst.D|TileComplexConst.L|TileComplexConst.R|TileComplexConst.UL|TileComplexConst.UR:
			case TileComplexConst.U|TileComplexConst.D|TileComplexConst.L|TileComplexConst.R|TileComplexConst.UL|TileComplexConst.DL:
			case TileComplexConst.U|TileComplexConst.D|TileComplexConst.L|TileComplexConst.R|TileComplexConst.UL|TileComplexConst.DR:
			case TileComplexConst.U|TileComplexConst.D|TileComplexConst.L|TileComplexConst.R|TileComplexConst.UR|TileComplexConst.DL:
			case TileComplexConst.U|TileComplexConst.D|TileComplexConst.L|TileComplexConst.R|TileComplexConst.UR|TileComplexConst.DR:
			case TileComplexConst.U|TileComplexConst.D|TileComplexConst.L|TileComplexConst.R|TileComplexConst.DL|TileComplexConst.DR:
			case TileComplexConst.U|TileComplexConst.D|TileComplexConst.L|TileComplexConst.R|TileComplexConst.UL|TileComplexConst.UR|TileComplexConst.DL:
			case TileComplexConst.U|TileComplexConst.D|TileComplexConst.L|TileComplexConst.R|TileComplexConst.UL|TileComplexConst.UR|TileComplexConst.DR:
			case TileComplexConst.U|TileComplexConst.D|TileComplexConst.L|TileComplexConst.R|TileComplexConst.UL|TileComplexConst.DL|TileComplexConst.DR:
			case TileComplexConst.U|TileComplexConst.D|TileComplexConst.L|TileComplexConst.R|TileComplexConst.UR|TileComplexConst.DL|TileComplexConst.DR:
			case TileComplexConst.U|TileComplexConst.D|TileComplexConst.L|TileComplexConst.R|TileComplexConst.UL|TileComplexConst.UR|TileComplexConst.DL|TileComplexConst.DR:
				GetAllTileItem (complex.updownleftright_4s, list);
				return;
			}
			GetAllTileItem (complex.none, list);
		}

		private	void GetTileItemForDirection (TileLibrary library, TileDirection direction, List<TileItemLink> list)
		{
			switch (face) {
			case TileFace.Up:
				GetAllTileItem (direction.up, list);
				break;
			case TileFace.Down:
				GetAllTileItem (direction.down, list);
				break;
			case TileFace.Left:
				GetAllTileItem (direction.left, list);
				break;
			case TileFace.Right:
				GetAllTileItem (direction.right, list);
				break;
			}
		}

		private	void GetTileItemForPerlin (TileLibrary library, TilePerlin perlin, List<TileItemLink> list)
		{
			if (map == null) {
				GetAllTileItem (perlin.links [0], list);
				return;
			}
			var density = Mathf.PerlinNoise (this.xf / (float)map.width, this.zf / (float)map.depth);
			var seg = (float)perlin.links.Length * density;
			GetAllTileItem (perlin.links [(int)seg], list);
		}

		private	void GetTileItemForRandom (TileLibrary library, TileRandom random, List<TileItemLink> list)
		{
			var index = this.valueRandom * (float)random.links.Length;
			index = Mathf.Min (index, random.links.Length-1);
			GetAllTileItem (random.links [(int)index], list);
		}
	}
}
