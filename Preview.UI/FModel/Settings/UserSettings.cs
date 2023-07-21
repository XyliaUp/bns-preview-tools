using System.IO;
using System.Windows;

using CUE4Parse.UE4.Assets.Exports.Material;
using CUE4Parse.UE4.Assets.Exports.Texture;
using CUE4Parse.UE4.Versions;

using CUE4Parse_Conversion.Meshes;
using CUE4Parse_Conversion.Textures;

using FModel.Framework;
using FModel.Views.Snooper;

using Newtonsoft.Json;

namespace FModel.Settings
{
	public sealed class UserSettings : ViewModel
    {
        public static UserSettings Default { get; set; }
#if DEBUG
        public static readonly string FilePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "FModel", "AppSettings_Debug.json");
#else
        public static readonly string FilePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "FModel", "AppSettings.json");
#endif

        static UserSettings()
        {
            Default = new UserSettings();
        }

        public static void Save()
        {
            File.WriteAllText(FilePath, JsonConvert.SerializeObject(Default, Formatting.Indented));
        }

        public static void Delete()
        {
            if (File.Exists(FilePath)) File.Delete(FilePath);
        }


        private bool _showChangelog = true;
        public bool ShowChangelog
        {
            get => _showChangelog;
            set => SetProperty(ref _showChangelog, value);
        }

        private string _outputDirectory;
        public string OutputDirectory
        {
            get => _outputDirectory;
            set => SetProperty(ref _outputDirectory, value);
        }

        private string _rawDataDirectory;
        public string RawDataDirectory
        {
            get => _rawDataDirectory;
            set => SetProperty(ref _rawDataDirectory, value);
        }

        private string _propertiesDirectory;
        public string PropertiesDirectory
        {
            get => _propertiesDirectory;
            set => SetProperty(ref _propertiesDirectory, value);
        }

        private string _textureDirectory;
        public string TextureDirectory
        {
            get => _textureDirectory;
            set => SetProperty(ref _textureDirectory, value);
        }

        private string _audioDirectory;
        public string AudioDirectory
        {
            get => _audioDirectory;
            set => SetProperty(ref _audioDirectory, value);
        }

        private string _modelDirectory;
        public string ModelDirectory
        {
            get => _modelDirectory;
            set => SetProperty(ref _modelDirectory, value);
        }

        private string _gameDirectory;
        public string GameDirectory
        {
            get => _gameDirectory;
            set => SetProperty(ref _gameDirectory, value);
        }

        private int _lastOpenedSettingTab;
        public int LastOpenedSettingTab
        {
            get => _lastOpenedSettingTab;
            set => SetProperty(ref _lastOpenedSettingTab, value);
        }

        private bool _isAutoOpenSounds = true;
        public bool IsAutoOpenSounds
        {
            get => _isAutoOpenSounds;
            set => SetProperty(ref _isAutoOpenSounds, value);
        }

        private bool _isLoggerExpanded = true;
        public bool IsLoggerExpanded
        {
            get => _isLoggerExpanded;
            set => SetProperty(ref _isLoggerExpanded, value);
        }

        private GridLength _avalonImageSize = new (200);
        public GridLength AvalonImageSize
        {
            get => _avalonImageSize;
            set => SetProperty(ref _avalonImageSize, value);
        }



        private ELanguage _assetLanguage = ELanguage.English;
        public ELanguage AssetLanguage
        {
            get => _assetLanguage;
            set => SetProperty(ref _assetLanguage, value);
        }



        private bool _cosmeticDisplayAsset;
        public bool CosmeticDisplayAsset
        {
            get => _cosmeticDisplayAsset;
            set => SetProperty(ref _cosmeticDisplayAsset, value);
        }

        private int _imageMergerMargin = 5;
        public int ImageMergerMargin
        {
            get => _imageMergerMargin;
            set => SetProperty(ref _imageMergerMargin, value);
        }

        private bool _readScriptData;
        public bool ReadScriptData
        {
            get => _readScriptData;
            set => SetProperty(ref _readScriptData, value);
        }



        private ETexturePlatform _overridedPlatform = ETexturePlatform.DesktopMobile;
        public ETexturePlatform OverridedPlatform
        {
            get => _overridedPlatform;
            set => SetProperty(ref _overridedPlatform, value);
        }



        private EMeshFormat _meshExportFormat = EMeshFormat.ActorX;
        public EMeshFormat MeshExportFormat
        {
            get => _meshExportFormat;
            set => SetProperty(ref _meshExportFormat, value);
        }

        private EMaterialFormat _materialExportFormat = EMaterialFormat.FirstLayer;
        public EMaterialFormat MaterialExportFormat
        {
            get => _materialExportFormat;
            set => SetProperty(ref _materialExportFormat, value);
        }

        private ETextureFormat _textureExportFormat = ETextureFormat.Png;
        public ETextureFormat TextureExportFormat
        {
            get => _textureExportFormat;
            set => SetProperty(ref _textureExportFormat, value);
        }

        private ESocketFormat _socketExportFormat = ESocketFormat.Bone;
        public ESocketFormat SocketExportFormat
        {
            get => _socketExportFormat;
            set => SetProperty(ref _socketExportFormat, value);
        }

        private ELodFormat _lodExportFormat = ELodFormat.FirstLod;
        public ELodFormat LodExportFormat
        {
            get => _lodExportFormat;
            set => SetProperty(ref _lodExportFormat, value);
        }

        private bool _showSkybox = true;
        public bool ShowSkybox
        {
            get => _showSkybox;
            set => SetProperty(ref _showSkybox, value);
        }

        private bool _showGrid = true;
        public bool ShowGrid
        {
            get => _showGrid;
            set => SetProperty(ref _showGrid, value);
        }

        private bool _animateWithRotationOnly;
        public bool AnimateWithRotationOnly
        {
            get => _animateWithRotationOnly;
            set => SetProperty(ref _animateWithRotationOnly, value);
        }

        private Camera.WorldMode _cameraMode = Camera.WorldMode.Arcball;
        public Camera.WorldMode CameraMode
        {
            get => _cameraMode;
            set => SetProperty(ref _cameraMode, value);
        }

        private int _previewMaxTextureSize = 1024;
        public int PreviewMaxTextureSize
        {
            get => _previewMaxTextureSize;
            set => SetProperty(ref _previewMaxTextureSize, value);
        }

        private bool _previewStaticMeshes = true;
        public bool PreviewStaticMeshes
        {
            get => _previewStaticMeshes;
            set => SetProperty(ref _previewStaticMeshes, value);
        }

        private bool _previewSkeletalMeshes = true;
        public bool PreviewSkeletalMeshes
        {
            get => _previewSkeletalMeshes;
            set => SetProperty(ref _previewSkeletalMeshes, value);
        }

        private bool _previewMaterials = true;
        public bool PreviewMaterials
        {
            get => _previewMaterials;
            set => SetProperty(ref _previewMaterials, value);
        }

        private bool _previewWorlds = true;
        public bool PreviewWorlds
        {
            get => _previewWorlds;
            set => SetProperty(ref _previewWorlds, value);
        }

        private bool _saveMorphTargets = true;
        public bool SaveMorphTargets
        {
            get => _saveMorphTargets;
            set => SetProperty(ref _saveMorphTargets, value);
        }

        private bool _saveSkeletonAsMesh;
        public bool SaveSkeletonAsMesh
        {
            get => _saveSkeletonAsMesh;
            set => SetProperty(ref _saveSkeletonAsMesh, value);
        }
    }
}
