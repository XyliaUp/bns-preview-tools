using System.Reflection;

using CUE4Parse.UE4.Assets.Exports.Material;

using CUE4Parse_Conversion.Meshes;
using CUE4Parse_Conversion.Textures;

using FModel.Views.Snooper;

using Xylia.Extension;
using Xylia.Preview.Properties;
using Xylia.Preview.UI.Controls;
using Xylia.Preview.UI.Views;

namespace Xylia.Preview.UI.ViewModels;
public partial class UserSettings : Settings
{
	public static UserSettings Default => new();


	#region Model
	private string _modelDirectory;
	public string ModelDirectory
	{
		get => _modelDirectory;
		set => SetProperty(ref _modelDirectory, value);
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

	private bool _saveEmbeddedMaterials = true;
	public bool SaveEmbeddedMaterials
	{
		get => _saveEmbeddedMaterials;
		set => SetProperty(ref _saveEmbeddedMaterials, value);
	}

	private bool _saveSkeletonAsMesh;
	public bool SaveSkeletonAsMesh
	{
		get => _saveSkeletonAsMesh;
		set => SetProperty(ref _saveSkeletonAsMesh, value);
	}
	#endregion

	#region Preview 
	protected const string sp = "Preview";

	public CopyMode CopyMode
	{
		get => (CopyMode)GetValue().ToInt32();
		set
		{
			SetValue((int)value);
			BnsCustomLabelWidget.CopyMode = value;
		}
	}


	public bool Text_LoadPrevious { get => GetValue().ToBool(); set => SetValue(value); }
	public string Text_OldPath { get => GetValue(); set => SetValue(value); }
	public string Text_NewPath { get => GetValue(); set => SetValue(value); }
	#endregion

	#region Common
	public bool UsePerformanceMonitor
	{
		get => GetValue().ToBool();
		set
		{
			SetValue(value);

			if (value) ProcessFloatWindow.Instance.Show();
			else ProcessFloatWindow.Instance.Close();
		}
	}
	#endregion
}

internal static class VersionHelper
{
	public static Version InternalVersion => Assembly.GetEntryAssembly().GetName().Version;

	public static DateTime Time => new DateTime(2000, 1, 1).AddDays(InternalVersion.Build).AddSeconds(InternalVersion.Revision * 2);

	public static Version Version => new Version(InternalVersion.Major, InternalVersion.Minor, InternalVersion.Build);
}