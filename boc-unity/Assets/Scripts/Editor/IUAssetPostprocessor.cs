using UnityEngine;
using System.Collections;
using UnityEditor;

namespace iu {

	public class IUAssetPostprocessor : AssetPostprocessor {

		//public void OnPreprocessModel() {
		//    var modelImporter = assetImporter as ModelImporter;
		//    if (assetPath.Contains("@")) {
		//        modelImporter.animationType = ModelImporterAnimationType.Legacy;
		//        modelImporter.importMaterials = false;
		//    }
		//}

		void OnPreprocessAudio() {
			var importer = assetImporter as AudioImporter;

		}

		void OnPreprocessTexture() {
			var textureImporter = assetImporter as TextureImporter;
			textureImporter.mipmapEnabled = false;

			var setting = textureImporter.GetDefaultPlatformTextureSettings();
			setting.textureCompression = TextureImporterCompression.Uncompressed;
			textureImporter.SetPlatformTextureSettings(setting);
		}

	}

}