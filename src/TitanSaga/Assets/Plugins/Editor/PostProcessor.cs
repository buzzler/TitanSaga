using UnityEngine;
using UnityEditor;
using UnityEditor.iOS.Xcode;

public class PostProcessor {
	public	static void Run(string buildPath) {
		if (EditorUserBuildSettings.activeBuildTarget == BuildTarget.Android) {
		}
		if (EditorUserBuildSettings.activeBuildTarget == BuildTarget.iOS) {
			InfoPlist (buildPath);
			PBX (buildPath);
		}
	}

	private	static void InfoPlist(string buildPath) {
	}

	private	static void PBX(string buildPath) {
		var pbxPath = PBXProject.GetPBXProjectPath (buildPath);
		var pbx = new PBXProject ();
		pbx.ReadFromFile (pbxPath);

		// Build Setting
		var guidNative = pbx.TargetGuidByName ("Unity-iPhone");
		pbx.AddBuildProperty (guidNative, "DEVELOPMENT_TEAM", "");
		pbx.AddBuildProperty (guidNative, "PROVISIONING_PROFILE_SPECIFIER", "");
		pbx.AddBuildProperty (guidNative, "CODE_SIGN_IDENTITY", "iPhone Developer");
		pbx.AddBuildProperty (guidNative, "CODE_SIGN_IDENTITY[sdk=iphoneos*]", "iPhone Developer");
//		pbx.AddBuildProperty (guidNative, "DEVELOPMENT_TEAM", "9685LCHPF2");
//		pbx.AddBuildProperty (guidNative, "PROVISIONING_PROFILE_SPECIFIER", "1b1d2f16-5ec1-435c-8e63-6ae03235d0f0");
//		pbx.AddBuildProperty (guidNative, "CODE_SIGN_IDENTITY", "iPhone Developer: KYUNGHYUN SHIM (XZ69MYYJ9V)");
//		pbx.AddBuildProperty (guidNative, "CODE_SIGN_IDENTITY[sdk=iphoneos*]", "iPhone Developer: KYUNGHYUN SHIM (XZ69MYYJ9V)");

		// Release Config Only
		//var guidConfigRelease = pbx.BuildConfigByName (guidNative, "Release");
		//pbx.AddBuildPropertyForConfig (guidConfigRelease, "key", "value");

		// Debug Config Only
		//var guidConfigDebug = pbx.BuildConfigByName (guidNative, "Debug");
		//pbx.AddBuildPropertyForConfig (guidConfigDebug, "key", "value");

		pbx.WriteToFile (pbxPath);
	}
}
