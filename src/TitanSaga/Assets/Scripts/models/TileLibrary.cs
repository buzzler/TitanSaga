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

		public	Dictionary<string, TileBase>			allItems;
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
			randoms = new Dictionary<string, TileRandom> ();

			allItems = new Dictionary<string, TileBase> ();
			items = new Dictionary<string, TileItem> ();
			itemLinks = new Dictionary<string, TileItemLink> ();
		}

		public	TileBase FindItem (string id)
		{
			if (allItems.ContainsKey (id)) {
				return allItems [id];
			} else {
				return null;
			}
		}
	}

	public	class TileBase
	{
		public	string	id;
		public	TileType type;

		public	virtual void Hashing() {
		}

		public	static TileItemLink[] SplitToLinks(string value, Dictionary<string, TileItemLink> dic)
		{
			var ranges = value.Split ("|" [0]);
			var capa = ranges.Length;
			var result = new TileItemLink[capa];
			for (var i = 0; i < capa ; i++) {
				result [i] = dic [(ranges [i])];
			}
			return result;
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
		private	TileItemLink[] _link;
		public	TileItemLink[] link { get { return _link; } }

		public	TileNormal(IDataReader dr, Dictionary<string, TileItemLink> dic) {
			this.type = TileType.Normal;
			this.id = dr.GetString (0);
			this._link = SplitToLinks (dr.GetString (1), dic);
		}
	}

	public	class TileCharacterMotion {
		private	string			_id;
		private	string			_behavior;
		private	TileItemLink[]	_up;
		private	TileItemLink[]	_down;
		private	TileItemLink[]	_left;
		private	TileItemLink[]	_right;
		public	string			id		{ get { return _id; } }
		public	string			behavior{ get { return _behavior; } }
		public	TileItemLink[]	up		{ get { return _up; } }
		public	TileItemLink[]	down	{ get { return _down; } }
		public	TileItemLink[]	left	{ get { return _left; } }
		public	TileItemLink[]	right	{ get { return _right; } }

		public	TileCharacterMotion(IDataReader dr, Dictionary<string, TileItemLink> dic) {
			this._id = dr.GetInt32 (0).ToString ();
			this._behavior = dr.GetString (1);
			this._up = TileBase.SplitToLinks (dr.GetString (2), dic);
			this._down = TileBase.SplitToLinks (dr.GetString (3), dic);
			this._left = TileBase.SplitToLinks (dr.GetString (4), dic);
			this._right = TileBase.SplitToLinks (dr.GetString (5), dic);
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
			this._walk = dic [dr.GetString (4)];
			this._attack = dic [dr.GetString (5)];
		}
	}

	public	class TileBuilding : TileBase {
		private	TileItemLink[]	_basic;
		private	TileItemLink[]	_blue;
		private	TileItemLink[]	_green;
		private	TileItemLink[]	_black;
		private	TileItemLink[]	_purple;
		private	TileItemLink[]	_red;
		private	TileItemLink[]	_yellow;
		public	TileItemLink[]	basic	{ get { return _basic; } }
		public	TileItemLink[]	blue	{ get { return _blue; } }
		public	TileItemLink[]	green	{ get { return _green; } }
		public	TileItemLink[]	black	{ get { return _black; } }
		public	TileItemLink[]	purple	{ get { return _purple; } }
		public	TileItemLink[]	red		{ get { return _red; } }
		public	TileItemLink[]	yellow	{ get { return _yellow; } }

		public	TileBuilding(IDataReader dr, Dictionary<string, TileItemLink> dic) {
			this.type = TileType.Building;
			this.id = dr.GetString (0);

			if (!dr.IsDBNull(1))
				this._basic = SplitToLinks (dr.GetString (1), dic);
			if (!dr.IsDBNull(2))
				this._blue = SplitToLinks (dr.GetString (2), dic);
			if (!dr.IsDBNull(3))
				this._green = SplitToLinks (dr.GetString (3), dic);
			if (!dr.IsDBNull(4))
				this._black = SplitToLinks (dr.GetString (4), dic);
			if (!dr.IsDBNull(5))
				this._purple = SplitToLinks (dr.GetString (5), dic);
			if (!dr.IsDBNull(6))
				this._red = SplitToLinks (dr.GetString (6), dic);
			if (!dr.IsDBNull(7))
				this._yellow = SplitToLinks (dr.GetString (7), dic);
		}
	}

	public	class TileWall : TileBase {
		private	TileItemLink[]	_basic;
		private	TileItemLink[]	_up;
		private	TileItemLink[]	_left;
		private	TileItemLink[]	_upleft;

		public	TileItemLink[]	basic	{ get { return _basic; } }
		public	TileItemLink[]	up		{ get { return _up; } }
		public	TileItemLink[]	left	{ get { return _left; } }
		public	TileItemLink[]	upleft	{ get { return _upleft; } }

		public	TileWall (IDataReader dr, Dictionary<string, TileItemLink> dic) {
			this.type = TileType.Wall;
			this.id = dr.GetString (0);
			this._basic = SplitToLinks (dr.GetString (1), dic);
			this._up = SplitToLinks (dr.GetString (2), dic);
			this._left = SplitToLinks (dr.GetString (3), dic);
			this._upleft = SplitToLinks (dr.GetString (4), dic);
		}
	}

	public	class TileSimple : TileBase {
		private	TileItemLink[]	_basic;
		private	TileItemLink[]	_u;
		private	TileItemLink[]	_d;
		private	TileItemLink[]	_l;
		private	TileItemLink[]	_r;
		private	TileItemLink[]	_ud;
		private	TileItemLink[]	_ul;
		private	TileItemLink[]	_ur;
		private	TileItemLink[]	_dl;
		private	TileItemLink[]	_dr;
		private	TileItemLink[]	_lr;
		private	TileItemLink[]	_udl;
		private	TileItemLink[]	_udr;
		private	TileItemLink[]	_ulr;
		private	TileItemLink[]	_dlr;
		private	TileItemLink[]	_udlr;

		public	TileItemLink[]	basic	{ get { return _basic; } }
		public	TileItemLink[]	u		{ get { return _u; } }
		public	TileItemLink[]	d		{ get { return _d; } }
		public	TileItemLink[]	l		{ get { return _l; } }
		public	TileItemLink[]	r		{ get { return _r; } }
		public	TileItemLink[]	ud		{ get { return _ud; } }
		public	TileItemLink[]	ul		{ get { return _ul; } }
		public	TileItemLink[]	ur		{ get { return _ur; } }
		public	TileItemLink[]	dl		{ get { return _dl; } }
		public	TileItemLink[]	dr		{ get { return _dr; } }
		public	TileItemLink[]	lr		{ get { return _lr; } }
		public	TileItemLink[]	udl		{ get { return _udl; } }
		public	TileItemLink[]	udr		{ get { return _udr; } }
		public	TileItemLink[]	ulr		{ get { return _ulr; } }
		public	TileItemLink[]	dlr		{ get { return _dlr; } }
		public	TileItemLink[]	udlr	{ get { return _udlr; } }

		public	TileSimple (IDataReader dr, Dictionary<string, TileItemLink> dic) {
			this.type = TileType.Simple;
			this.id = dr.GetString (0);
			this._basic = SplitToLinks (dr.GetString (1), dic);
			this._u = SplitToLinks (dr.GetString (2), dic);
			this._d = SplitToLinks (dr.GetString (3), dic);
			this._l = SplitToLinks (dr.GetString (4), dic);
			this._r = SplitToLinks (dr.GetString (5), dic);
			this._ud = SplitToLinks (dr.GetString (6), dic);
			this._ul = SplitToLinks (dr.GetString (7), dic);
			this._ur = SplitToLinks (dr.GetString (8), dic);
			this._dl = SplitToLinks (dr.GetString (9), dic);
			this._dr = SplitToLinks (dr.GetString (10), dic);
			this._lr = SplitToLinks (dr.GetString (11), dic);
			this._udl = SplitToLinks (dr.GetString (12), dic);
			this._udr = SplitToLinks (dr.GetString (13), dic);
			this._ulr = SplitToLinks (dr.GetString (14), dic);
			this._dlr = SplitToLinks (dr.GetString (15), dic);
			this._udlr = SplitToLinks (dr.GetString (16), dic);
		}
	}

	public	class TileComplex : TileBase
	{
		private	TileItemLink[]	_basic;
		private	TileItemLink[]	_u_1c;
		private	TileItemLink[]	_d_1c;
		private	TileItemLink[]	_l_1c;
		private	TileItemLink[]	_r_1c;
		private	TileItemLink[]	_u_1s;
		private	TileItemLink[]	_d_1s;
		private	TileItemLink[]	_l_1s;
		private	TileItemLink[]	_r_1s;
		private	TileItemLink[]	_lu_1c1s;
		private	TileItemLink[]	_du_1c1s;
		private	TileItemLink[]	_rd_1c1s;
		private	TileItemLink[]	_ud_1c1s;
		private	TileItemLink[]	_rl_1c1s;
		private	TileItemLink[]	_dl_1c1s;
		private	TileItemLink[]	_lr_1c1s;
		private	TileItemLink[]	_ur_1c1s;
		private	TileItemLink[]	_dlu_2c1s;
		private	TileItemLink[]	_urd_2c1s;
		private	TileItemLink[]	_drl_2c1s;
		private	TileItemLink[]	_ulr_2c1s;
		private	TileItemLink[]	_dl_2c;
		private	TileItemLink[]	_ur_2c;
		private	TileItemLink[]	_ud_2c;
		private	TileItemLink[]	_lr_2c;
		private	TileItemLink[]	_ul_2c;
		private	TileItemLink[]	_dr_2c;
		private	TileItemLink[]	_ur_2s;
		private	TileItemLink[]	_dr_2s;
		private	TileItemLink[]	_dl_2s;
		private	TileItemLink[]	_ul_2s;
		private	TileItemLink[]	_ud_2s;
		private	TileItemLink[]	_lr_2s;
		private	TileItemLink[]	_udr_1c2s;
		private	TileItemLink[]	_rdl_1c2s;
		private	TileItemLink[]	_dul_1c2s;
		private	TileItemLink[]	_lur_1c2s;
		private	TileItemLink[]	_dlr_3c;
		private	TileItemLink[]	_udr_3c;
		private	TileItemLink[]	_udl_3c;
		private	TileItemLink[]	_ulr_3c;
		private	TileItemLink[]	_dlr_3s;
		private	TileItemLink[]	_ulr_3s;
		private	TileItemLink[]	_udr_3s;
		private	TileItemLink[]	_udl_3s;
		private	TileItemLink[]	_udlr_4c;
		private	TileItemLink[]	_udlr_4s;

		public	TileItemLink[]	basic		{ get { return _basic; } }
		public	TileItemLink[]	u_1c		{ get { return _u_1c; } }
		public	TileItemLink[]	d_1c		{ get { return _d_1c; } }
		public	TileItemLink[]	l_1c		{ get { return _l_1c; } }
		public	TileItemLink[]	r_1c		{ get { return _r_1c; } }
		public	TileItemLink[]	u_1s		{ get { return _u_1s; } }
		public	TileItemLink[]	d_1s		{ get { return _d_1s; } }
		public	TileItemLink[]	l_1s		{ get { return _l_1s; } }
		public	TileItemLink[]	r_1s		{ get { return _r_1s; } }
		public	TileItemLink[]	lu_1c1s		{ get { return _lu_1c1s; } }
		public	TileItemLink[]	du_1c1s		{ get { return _du_1c1s; } }
		public	TileItemLink[]	rd_1c1s		{ get { return _rd_1c1s; } }
		public	TileItemLink[]	ud_1c1s		{ get { return _ud_1c1s; } }
		public	TileItemLink[]	rl_1c1s		{ get { return _rl_1c1s; } }
		public	TileItemLink[]	dl_1c1s		{ get { return _dl_1c1s; } }
		public	TileItemLink[]	lr_1c1s		{ get { return _lr_1c1s; } }
		public	TileItemLink[]	ur_1c1s		{ get { return _ur_1c1s; } }
		public	TileItemLink[]	dlu_2c1s		{ get { return _dlu_2c1s; } }
		public	TileItemLink[]	urd_2c1s		{ get { return _urd_2c1s; } }
		public	TileItemLink[]	drl_2c1s		{ get { return _drl_2c1s; } }
		public	TileItemLink[]	ulr_2c1s		{ get { return _ulr_2c1s; } }
		public	TileItemLink[]	dl_2c		{ get { return _dl_2c; } }
		public	TileItemLink[]	ur_2c		{ get { return _ur_2c; } }
		public	TileItemLink[]	ud_2c		{ get { return _ud_2c; } }
		public	TileItemLink[]	lr_2c		{ get { return _lr_2c; } }
		public	TileItemLink[]	ul_2c		{ get { return _ul_2c; } }
		public	TileItemLink[]	dr_2c		{ get { return _dr_2c; } }
		public	TileItemLink[]	ur_2s		{ get { return _ur_2s; } }
		public	TileItemLink[]	dr_2s		{ get { return _dr_2s; } }
		public	TileItemLink[]	dl_2s		{ get { return _dl_2s; } }
		public	TileItemLink[]	ul_2s		{ get { return _ul_2s; } }
		public	TileItemLink[]	ud_2s		{ get { return _ud_2s; } }
		public	TileItemLink[]	lr_2s		{ get { return _lr_2s; } }
		public	TileItemLink[]	udr_1c2s		{ get { return _udr_1c2s; } }
		public	TileItemLink[]	rdl_1c2s		{ get { return _rdl_1c2s; } }
		public	TileItemLink[]	dul_1c2s		{ get { return _dul_1c2s; } }
		public	TileItemLink[]	lur_1c2s		{ get { return _lur_1c2s; } }

		public	TileItemLink[]	dlr_3c		{ get { return _dlr_3c; } }
		public	TileItemLink[]	udr_3c		{ get { return _udr_3c; } }
		public	TileItemLink[]	udl_3c		{ get { return _udl_3c; } }
		public	TileItemLink[]	ulr_3c		{ get { return _ulr_3c; } }
		public	TileItemLink[]	dlr_3s		{ get { return _dlr_3s; } }
		public	TileItemLink[]	ulr_3s		{ get { return _ulr_3s; } }
		public	TileItemLink[]	udr_3s		{ get { return _udr_3s; } }
		public	TileItemLink[]	udl_3s		{ get { return _udl_3s; } }
		public	TileItemLink[]	udlr_4c		{ get { return _udlr_4c; } }
		public	TileItemLink[]	udlr_4s		{ get { return _udlr_4s; } }

		public	TileComplex (IDataReader dr, Dictionary<string, TileItemLink> dic)
		{
			type = TileType.Complex;
			this.id = dr.GetString (0);
			this._basic = SplitToLinks (dr.GetString (1), dic);
			this._u_1c = SplitToLinks (dr.GetString (2), dic);
			this._d_1c = SplitToLinks (dr.GetString (3), dic);
			this._l_1c = SplitToLinks (dr.GetString (4), dic);
			this._r_1c = SplitToLinks (dr.GetString (5), dic);
			this._u_1s = SplitToLinks (dr.GetString (6), dic);
			this._d_1s = SplitToLinks (dr.GetString (7), dic);
			this._l_1s = SplitToLinks (dr.GetString (8), dic);
			this._r_1s = SplitToLinks (dr.GetString (9), dic);
			this._lu_1c1s = SplitToLinks (dr.GetString (10), dic);
			this._du_1c1s = SplitToLinks (dr.GetString (11), dic);
			this._rd_1c1s = SplitToLinks (dr.GetString (12), dic);
			this._ud_1c1s = SplitToLinks (dr.GetString (13), dic);
			this._rl_1c1s = SplitToLinks (dr.GetString (14), dic);
			this._dl_1c1s = SplitToLinks (dr.GetString (15), dic);
			this._lr_1c1s = SplitToLinks (dr.GetString (16), dic);
			this._ur_1c1s = SplitToLinks (dr.GetString (17), dic);
			this._dlu_2c1s = SplitToLinks (dr.GetString (18), dic);
			this._urd_2c1s = SplitToLinks (dr.GetString (19), dic);
			this._drl_2c1s = SplitToLinks (dr.GetString (20), dic);
			this._ulr_2c1s = SplitToLinks (dr.GetString (21), dic);
			this._dl_2c = SplitToLinks (dr.GetString (22), dic);
			this._ur_2c = SplitToLinks (dr.GetString (23), dic);
			this._ud_2c = SplitToLinks (dr.GetString (24), dic);
			this._lr_2c = SplitToLinks (dr.GetString (25), dic);
			this._ul_2c = SplitToLinks (dr.GetString (26), dic);
			this._dr_2c = SplitToLinks (dr.GetString (27), dic);
			this._ur_2s = SplitToLinks (dr.GetString (28), dic);
			this._dr_2s = SplitToLinks (dr.GetString (29), dic);
			this._dl_2s = SplitToLinks (dr.GetString (30), dic);
			this._ul_2s = SplitToLinks (dr.GetString (31), dic);
			this._ud_2s = SplitToLinks (dr.GetString (32), dic);
			this._lr_2s = SplitToLinks (dr.GetString (33), dic);
			this._udr_1c2s = SplitToLinks (dr.GetString (34), dic);
			this._rdl_1c2s = SplitToLinks (dr.GetString (35), dic);
			this._dul_1c2s = SplitToLinks (dr.GetString (36), dic);
			this._lur_1c2s = SplitToLinks (dr.GetString (37), dic);
			this._dlr_3c = SplitToLinks (dr.GetString (38), dic);
			this._udr_3c = SplitToLinks (dr.GetString (39), dic);
			this._udl_3c = SplitToLinks (dr.GetString (40), dic);
			this._ulr_3c = SplitToLinks (dr.GetString (41), dic);
			this._dlr_3s = SplitToLinks (dr.GetString (42), dic);
			this._ulr_3s = SplitToLinks (dr.GetString (43), dic);
			this._udr_3s = SplitToLinks (dr.GetString (44), dic);
			this._udl_3s = SplitToLinks (dr.GetString (45), dic);
			this._udlr_4c = SplitToLinks (dr.GetString (46), dic);
			this._udlr_4s = SplitToLinks (dr.GetString (47), dic);
		}
	}

	public	class TilePerlin : TileBase
	{
		private	float				_scale;
		private	TileItemLink[][]	_links;
		private	TileItemLink[]	_range00_05;
		private	TileItemLink[]	_range05_10;
		private	TileItemLink[]	_range10_15;
		private	TileItemLink[]	_range15_20;
		private	TileItemLink[]	_range20_25;
		private	TileItemLink[]	_range25_30;
		private	TileItemLink[]	_range30_35;
		private	TileItemLink[]	_range35_40;
		private	TileItemLink[]	_range40_45;
		private	TileItemLink[]	_range45_50;
		private	TileItemLink[]	_range50_55;
		private	TileItemLink[]	_range55_60;
		private	TileItemLink[]	_range60_65;
		private	TileItemLink[]	_range65_70;
		private	TileItemLink[]	_range70_75;
		private	TileItemLink[]	_range75_80;
		private	TileItemLink[]	_range80_85;
		private	TileItemLink[]	_range85_90;
		private	TileItemLink[]	_range90_95;
		private	TileItemLink[]	_range95_00;
		public	float			scale	{ get { return _scale; } }

		public	TilePerlin(IDataReader dr, Dictionary<string, TileItemLink> dic)
		{
			this.type = TileType.Perlin;
			this.id = dr.GetString (0);
			this._scale = dr.GetFloat (1);
			this._range00_05 = SplitToLinks (dr.GetString (2), dic);
			this._range05_10 = SplitToLinks (dr.GetString (3), dic);
			this._range10_15 = SplitToLinks (dr.GetString (4), dic);
			this._range15_20 = SplitToLinks (dr.GetString (5), dic);
			this._range20_25 = SplitToLinks (dr.GetString (6), dic);
			this._range25_30 = SplitToLinks (dr.GetString (7), dic);
			this._range30_35 = SplitToLinks (dr.GetString (8), dic);
			this._range35_40 = SplitToLinks (dr.GetString (9), dic);
			this._range40_45 = SplitToLinks (dr.GetString (10), dic);
			this._range45_50 = SplitToLinks (dr.GetString (11), dic);
			this._range50_55 = SplitToLinks (dr.GetString (12), dic);
			this._range55_60 = SplitToLinks (dr.GetString (13), dic);
			this._range60_65 = SplitToLinks (dr.GetString (14), dic);
			this._range65_70 = SplitToLinks (dr.GetString (15), dic);
			this._range70_75 = SplitToLinks (dr.GetString (16), dic);
			this._range75_80 = SplitToLinks (dr.GetString (17), dic);
			this._range80_85 = SplitToLinks (dr.GetString (18), dic);
			this._range85_90 = SplitToLinks (dr.GetString (19), dic);
			this._range90_95 = SplitToLinks (dr.GetString (20), dic);
			this._range95_00 = SplitToLinks (dr.GetString (21), dic);

			this._links = new TileItemLink[20][];
			this._links [0] = this._range00_05;
			this._links [1] = this._range05_10;
			this._links [2] = this._range10_15;
			this._links [3] = this._range15_20;
			this._links [4] = this._range20_25;
			this._links [5] = this._range25_30;
			this._links [6] = this._range30_35;
			this._links [7] = this._range35_40;
			this._links [8] = this._range40_45;
			this._links [9] = this._range45_50;
			this._links [10] = this._range50_55;
			this._links [11] = this._range55_60;
			this._links [12] = this._range60_65;
			this._links [13] = this._range65_70;
			this._links [14] = this._range70_75;
			this._links [15] = this._range75_80;
			this._links [16] = this._range80_85;
			this._links [17] = this._range85_90;
			this._links [18] = this._range90_95;
			this._links [19] = this._range95_00;
		}

		public	TileItemLink[]	GetTileItemLink (float value) {
			return this._links [(Mathf.Clamp((int)(value * 20f), 0, 19))];
		}

		public	TileItemLink[] GetTileItemLInk (int value) {
			return this._links [(int)Mathf.Clamp ((float)value / 5f, 0f, 19f)];
		}
	}

	public	class TileRandom : TileBase
	{
		private	int					_count;
		private	TileItemLink[][]	_links;

		public	TileRandom(IDataReader dr, Dictionary<string, TileItemLink> dic)
		{
			this.type = TileType.Random;
			this.id = dr.GetString (0);

			var ary = dr.GetString (1).Split ("\\"[0]);
			_count = ary.Length;
			this._links = new TileItemLink[_count][];
			for (int i = 0 ; i < _count ; i++) {
				this._links [i] = SplitToLinks (ary [i], dic);
			}
		}

		public	TileItemLink[] GetTileItemLink() {
			return _links [Random.Range (0, _count - 1)];
		}
	}

	public	class TileDirection : TileBase
	{
		private	TileItemLink[]	_u;
		private	TileItemLink[]	_d;
		private	TileItemLink[]	_l;
		private	TileItemLink[]	_r;
		public	TileItemLink[]	u	{ get { return _u; } }
		public	TileItemLink[]	d	{ get { return _d; } }
		public	TileItemLink[]	l	{ get { return _l; } }
		public	TileItemLink[]	r	{ get { return _r; } }

		public	TileDirection (IDataReader dr, Dictionary<string, TileItemLink> dic)
		{
			this.type = TileType.Direction;
			this.id = dr.GetString (0);
			this._u = SplitToLinks (dr.GetString (1), dic);
			this._d = SplitToLinks (dr.GetString (2), dic);
			this._l = SplitToLinks (dr.GetString (3), dic);
			this._r = SplitToLinks (dr.GetString (4), dic);
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
		private	TileItem[]						_itemArray;

		public	string							id			{ get { return _id; } }
		public	TileItemDirection				direction	{ get { return _direction; } }
		public	TileItemAniType					aniType		{ get { return _aniType; } }
		public	float							fps			{ get { return _fps; } }
		public	bool							flipV		{ get { return _flipV; } }
		public	bool							flipH		{ get { return _flipH; } }
		public	float							x			{ get { return _x; } }
		public	float							y			{ get { return _y; } }
		public	Dictionary<string, TileItem>	items		{ get { return _items; } }
		public	TileItem[]						itemArray	{ get { return _itemArray; } }
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

			int total = list.Count;
			_itemArray = new TileItem[total];
			_items = new Dictionary<string, TileItem>(total);
			for (int i = 0; i < total; i++) {
				var current = list [i];
				var tile = dic [current];
				if (_item == null) {
					_item = tile;
				}

				_itemArray [i] = tile;
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
			try {
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
					default:
						Debug.LogWarningFormat ("Unkown group: {0}", obj.group);
						break;
					}
					return true;
				} else {
					Debug.LogWarningFormat ("Conflict x:{0}, y:{1}, z:{2}", obj.x, obj.y, obj.z);
					return false;
				}
			} catch (System.Exception e) {
				Debug.LogWarningFormat ("Adding Tile: {0}", e.Message);
				return false;
			}
		}

		public	bool RemoveTileObject (TileObject obj)
		{
			try {
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
			} catch (System.Exception e) {
				Debug.LogWarningFormat ("Removing tile: {0}", e.Message);
				return false;
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
			this.group = TileGroup.Unit;
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
				list.AddRange ((tb as TileNormal).link);
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
				list.AddRange (building.basic);
				break;
			case TileColor.Blue:
				list.AddRange (building.blue);
				break;
			case TileColor.Green:
				list.AddRange (building.green);
				break;
			case TileColor.Black:
				list.AddRange (building.black);
				break;
			case TileColor.Purple:
				list.AddRange (building.purple);
				break;
			case TileColor.Red:
				list.AddRange (building.red);
				break;
			case TileColor.Yellow:
				list.AddRange (building.yellow);
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
				list.AddRange (wall.up);
				break;
			case TileWallConst.L:
				list.AddRange (wall.left);
				break;
			case TileWallConst.U_L:
				list.AddRange (wall.upleft);
				break;
			default:
				list.AddRange (wall.basic);
				break;
			}
		}

		private	void GetTileItemForCharacter (TileCharacter character, List<TileItemLink> list) {
			switch (face) {
			case TileFace.Up:
				list.AddRange (character.basic.up);
				break;
			case TileFace.Down:
				list.AddRange (character.basic.down);
				break;
			case TileFace.Left:
				list.AddRange (character.basic.left);
				break;
			case TileFace.Right:
				list.AddRange (character.basic.right);
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
				list.AddRange ((tb as TileNormal).link);
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
				list.AddRange (simple.u);
				break;
			case TileSimpleConst.D:
				list.AddRange (simple.d);
				break;
			case TileSimpleConst.L:
				list.AddRange (simple.l);
				break;
			case TileSimpleConst.R:
				list.AddRange (simple.r);
				break;
			case TileSimpleConst.U_D:
				list.AddRange (simple.ud);
				break;
			case TileSimpleConst.U_L:
				list.AddRange (simple.ul);
				break;
			case TileSimpleConst.U_R:
				list.AddRange (simple.ur);
				break;
			case TileSimpleConst.D_L:
				list.AddRange (simple.dl);
				break;
			case TileSimpleConst.D_R:
				list.AddRange (simple.dr);
				break;
			case TileSimpleConst.L_R:
				list.AddRange (simple.lr);
				break;
			case TileSimpleConst.U_D_L:
				list.AddRange (simple.udl);
				break;
			case TileSimpleConst.U_D_R:
				list.AddRange (simple.udr);
				break;
			case TileSimpleConst.U_L_R:
				list.AddRange (simple.ulr);
				break;
			case TileSimpleConst.D_L_R:
				list.AddRange (simple.dlr);
				break;
			case TileSimpleConst.U_D_L_R:
				list.AddRange (simple.udlr);
				break;
			default:
				list.AddRange (simple.basic);
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
				list.AddRange (complex.basic);
				return;
			case TileComplexConst.UL:
				list.AddRange (complex.u_1c);
				return;
			case TileComplexConst.DR:
				list.AddRange (complex.d_1c);
				return;
			case TileComplexConst.DL:
				list.AddRange (complex.l_1c);
				return;
			case TileComplexConst.UR:
				list.AddRange (complex.r_1c);
				return;
			case TileComplexConst.U:
			case TileComplexConst.U_UL:
			case TileComplexConst.U_UR:
			case TileComplexConst.U_UL_UR:
				list.AddRange (complex.u_1s);
				return;
			case TileComplexConst.D:
			case TileComplexConst.D_DL:
			case TileComplexConst.D_DR:
			case TileComplexConst.D_DL_DR:
				list.AddRange (complex.d_1s);
				return;
			case TileComplexConst.L:
			case TileComplexConst.L_UL:
			case TileComplexConst.L_DL:
			case TileComplexConst.L_UL_DL:
				list.AddRange (complex.l_1s);
				return;
			case TileComplexConst.R:
			case TileComplexConst.R_UR:
			case TileComplexConst.R_DR:
			case TileComplexConst.R_UR_DR:
				list.AddRange (complex.r_1s);
				return;
			case TileComplexConst.U_DL:
			case TileComplexConst.U_DL_UL:
			case TileComplexConst.U_DL_UR:
			case TileComplexConst.U_DL_UL_UR:
				list.AddRange (complex.lu_1c1s);
				return;
			case TileComplexConst.U_DR:
			case TileComplexConst.U_DR_UL:
			case TileComplexConst.U_DR_UR:
			case TileComplexConst.U_DR_UL_UR:
				list.AddRange (complex.du_1c1s);
				return;
			case TileComplexConst.D_UR:
			case TileComplexConst.D_UR_DL:
			case TileComplexConst.D_UR_DR:
			case TileComplexConst.D_UR_DL_DR:
				list.AddRange (complex.rd_1c1s);
				return;
			case TileComplexConst.D_UL:
			case TileComplexConst.D_UL_DL:
			case TileComplexConst.D_UL_DR:
			case TileComplexConst.D_UL_DL_DR:
				list.AddRange (complex.ud_1c1s);
				return;
			case TileComplexConst.L_UR:
			case TileComplexConst.L_UR_UL:
			case TileComplexConst.L_UR_DL:
			case TileComplexConst.L_UR_UL_DL:
				list.AddRange (complex.rl_1c1s);
				return;
			case TileComplexConst.L_DR:
			case TileComplexConst.L_DR_UL:
			case TileComplexConst.L_DR_DL:
			case TileComplexConst.L_DR_UL_DL:
				list.AddRange (complex.dl_1c1s);
				return;
			case TileComplexConst.R_DL:
			case TileComplexConst.R_DL_UR:
			case TileComplexConst.R_DL_DR:
			case TileComplexConst.R_DL_UR_DR:
				list.AddRange (complex.lr_1c1s);
				return;
			case TileComplexConst.R_UL:
			case TileComplexConst.R_UL_UR:
			case TileComplexConst.R_UL_DR:
			case TileComplexConst.R_UL_UR_DR:
				list.AddRange (complex.ur_1c1s);
				return;
			case TileComplexConst.U_DL_DR:
			case TileComplexConst.U_DL_DR_UL:
			case TileComplexConst.U_DL_DR_UR:
			case TileComplexConst.U_DL_DR_UL_UR:
				list.AddRange (complex.dlu_2c1s);
				return;
			case TileComplexConst.D_UL_UR:
			case TileComplexConst.D_UL_UR_DL:
			case TileComplexConst.D_UL_UR_DR:
			case TileComplexConst.D_UL_UR_DL_DR:
				list.AddRange (complex.urd_2c1s);
				return;
			case TileComplexConst.L_UR_DR:
			case TileComplexConst.L_UR_DR_UL:
			case TileComplexConst.L_UR_DR_DL:
			case TileComplexConst.L_UR_DR_UL_DL:
				list.AddRange (complex.drl_2c1s);
				return;
			case TileComplexConst.R_UL_DL:
			case TileComplexConst.R_UL_DL_UR:
			case TileComplexConst.R_UL_DL_DR:
			case TileComplexConst.R_UL_DL_UR_DR:
				list.AddRange (complex.ulr_2c1s);
				return;
			case TileComplexConst.DL_DR:
				list.AddRange (complex.dl_2c);
				return;
			case TileComplexConst.UL_UR:
				list.AddRange (complex.ur_2c);
				return;
			case TileComplexConst.UL_DR:
				list.AddRange (complex.ud_2c);
				return;
			case TileComplexConst.DL_UR:
				list.AddRange (complex.lr_2c);
				return;
			case TileComplexConst.UL_DL:
				list.AddRange (complex.ul_2c);
				return;
			case TileComplexConst.UR_DR:
				list.AddRange (complex.dr_2c);
				return;
			case TileComplexConst.U_R:
			case TileComplexConst.U_R_UL:
			case TileComplexConst.U_R_UR:
			case TileComplexConst.U_R_DR:
			case TileComplexConst.U_R_UL_UR:
			case TileComplexConst.U_R_UL_DR:
			case TileComplexConst.U_R_UR_DR:
			case TileComplexConst.U_R_UL_UR_DR:
				list.AddRange (complex.ur_2s);
				return;
			case TileComplexConst.D_R:
			case TileComplexConst.D_R_DL:
			case TileComplexConst.D_R_UR:
			case TileComplexConst.D_R_DR:
			case TileComplexConst.D_R_DL_UR:
			case TileComplexConst.D_R_DL_DR:
			case TileComplexConst.D_R_UR_DR:
			case TileComplexConst.D_R_DL_UR_DR:
				list.AddRange (complex.dr_2s);
				return;
			case TileComplexConst.D_L:
			case TileComplexConst.D_L_DL:
			case TileComplexConst.D_L_UL:
			case TileComplexConst.D_L_DR:
			case TileComplexConst.D_L_DL_UL:
			case TileComplexConst.D_L_DL_DR:
			case TileComplexConst.D_L_UL_DR:
			case TileComplexConst.D_L_DL_UL_DR:
				list.AddRange (complex.dl_2s);
				return;
			case TileComplexConst.U_L:
			case TileComplexConst.U_L_DL:
			case TileComplexConst.U_L_UL:
			case TileComplexConst.U_L_UR:
			case TileComplexConst.U_L_DL_UL:
			case TileComplexConst.U_L_DL_UR:
			case TileComplexConst.U_L_UL_UR:
			case TileComplexConst.U_L_DL_UL_UR:
				list.AddRange (complex.ul_2s);
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
				list.AddRange (complex.ud_2s);
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
				list.AddRange (complex.lr_2s);
				return;
			case TileComplexConst.D_R_UL:
			case TileComplexConst.D_R_UL_UR:
			case TileComplexConst.D_R_UL_DL:
			case TileComplexConst.D_R_UL_DR:
			case TileComplexConst.D_R_UL_UR_DL:
			case TileComplexConst.D_R_UL_UR_DR:
			case TileComplexConst.D_R_UL_DL_DR:
			case TileComplexConst.D_R_UL_UR_DL_DR:
				list.AddRange (complex.udr_1c2s);
				return;
			case TileComplexConst.D_L_UR:
			case TileComplexConst.D_L_UR_UL:
			case TileComplexConst.D_L_UR_DL:
			case TileComplexConst.D_L_UR_DR:
			case TileComplexConst.D_L_UR_UL_DL:
			case TileComplexConst.D_L_UR_UL_DR:
			case TileComplexConst.D_L_UR_DL_DR:
			case TileComplexConst.D_L_UR_UL_DL_DR:
				list.AddRange (complex.rdl_1c2s);
				return;
			case TileComplexConst.U_L_DR:
			case TileComplexConst.U_L_DR_UL:
			case TileComplexConst.U_L_DR_DL:
			case TileComplexConst.U_L_DR_UR:
			case TileComplexConst.U_L_DR_UL_DL:
			case TileComplexConst.U_L_DR_UL_UR:
			case TileComplexConst.U_L_DR_DL_UR:
			case TileComplexConst.U_L_DR_UL_DL_UR:
				list.AddRange (complex.dul_1c2s);
				return;
			case TileComplexConst.U_R_DL:
			case TileComplexConst.U_R_DL_UL:
			case TileComplexConst.U_R_DL_DR:
			case TileComplexConst.U_R_DL_UR:
			case TileComplexConst.U_R_DL_UL_DR:
			case TileComplexConst.U_R_DL_UL_UR:
			case TileComplexConst.U_R_DL_DR_UR:
			case TileComplexConst.U_R_DL_UL_DR_UR:
				list.AddRange (complex.lur_1c2s);
				return;
			case TileComplexConst.UR|TileComplexConst.DL|TileComplexConst.DR:
				list.AddRange (complex.dlr_3c);
				return;
			case TileComplexConst.UL|TileComplexConst.UR|TileComplexConst.DR:
				list.AddRange (complex.udr_3c);
				return;
			case TileComplexConst.UL|TileComplexConst.DL|TileComplexConst.DR:
				list.AddRange (complex.udl_3c);
				return;
			case TileComplexConst.UL|TileComplexConst.UR|TileComplexConst.DL:
				list.AddRange (complex.ulr_3c);
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
				list.AddRange (complex.dlr_3s);
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
				list.AddRange (complex.ulr_3s);
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
				list.AddRange (complex.udr_3s);
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
				list.AddRange (complex.udl_3s);
				return;
			case TileComplexConst.UL|TileComplexConst.UR|TileComplexConst.DL|TileComplexConst.DR:
				list.AddRange (complex.udlr_4c);
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
				list.AddRange (complex.udlr_4s);
				return;
			}
			list.AddRange (complex.basic);
		}

		private	void GetTileItemForDirection (TileLibrary library, TileDirection direction, List<TileItemLink> list)
		{
			switch (face) {
			case TileFace.Up:
				list.AddRange (direction.u);
				break;
			case TileFace.Down:
				list.AddRange (direction.d);
				break;
			case TileFace.Left:
				list.AddRange (direction.l);
				break;
			case TileFace.Right:
				list.AddRange (direction.r);
				break;
			}
		}

		private	void GetTileItemForPerlin (TileLibrary library, TilePerlin perlin, List<TileItemLink> list)
		{
			if (map == null) {
				list.AddRange (perlin.GetTileItemLInk(0));
				return;
			}
			var density = Mathf.PerlinNoise (this.xf / (float)map.width, this.zf / (float)map.depth);
			list.AddRange (perlin.GetTileItemLink (density));
		}

		private	void GetTileItemForRandom (TileLibrary library, TileRandom random, List<TileItemLink> list)
		{
			list.AddRange (random.GetTileItemLink ());
		}
	}
}
