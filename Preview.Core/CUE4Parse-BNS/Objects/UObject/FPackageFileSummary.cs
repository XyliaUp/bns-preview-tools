using CUE4Parse.UE4.Writers;

namespace CUE4Parse.UE4.Objects.UObject;
public static partial class FPackageFileSummaryEx
{
	public static void Serialize(this FPackageFileSummary @this, FArchiveWriter writer)
	{
		writer.Write(@this.Tag);

		/*
		* The package file version number when this package was saved.
		*
		* Lower 16 bits stores the UE3 engine version
		* Upper 16 bits stores the UE4/licensee version
		* For newer packages this is -7
		*		-2 indicates presence of enum-based custom versions
		*		-3 indicates guid-based custom versions
		*		-4 indicates removal of the UE3 version. Packages saved with this ID cannot be loaded in older engine versions
		*		-5 indicates the replacement of writing out the "UE3 version" so older versions of engine can gracefully fail to open newer packages
		*		-6 indicates optimizations to how custom versions are being serialized
		*		-7 indicates the texture allocation info has been removed from the summary
		*		-8 indicates that the UE5 version has been added to the summary
		*/
		int legacyFileVersion = -7;
		writer.Write(legacyFileVersion);

		if (legacyFileVersion < 0) // means we have modern version numbers
		{
			if (legacyFileVersion != -4)
			{
				var legacyUE3Version = 0x00000360;
				writer.Write(legacyUE3Version);
			}

			writer.Write(@this.FileVersionUE.FileVersionUE4);

			if (legacyFileVersion <= -8)
			{
				writer.Write(@this.FileVersionUE.FileVersionUE5);
			}

			writer.Write((byte)@this.FileVersionLicenseeUE);
			//CustomVersionContainer = legacyFileVersion <= -2 ? new FCustomVersionContainer(Ar) : new FCustomVersionContainer();

			//if (Ar.Versions.CustomVersions == null && CustomVersionContainer.Versions.Length > 0)
			//{
			//	Ar.Versions.CustomVersions = CustomVersionContainer;
			//}

			//if (FileVersionUE.FileVersionUE4 == 0 && FileVersionUE.FileVersionUE5 == 0 && FileVersionLicenseeUE == 0)
			//{
			//	// this file is unversioned, remember that, then use current versions
			//	bUnversioned = true;
			//	FileVersionUE = Ar.Ver;
			//	FileVersionLicenseeUE = EUnrealEngineObjectLicenseeUEVersion.VER_LIC_AUTOMATIC_VERSION;
			//}
			//else
			//{
			//	bUnversioned = false;
			//	// Only apply the version if an explicit version is not set
			//	if (!Ar.Versions.bExplicitVer)
			//	{
			//		Ar.Ver = FileVersionUE;
			//	}
			//}
		}

		return;

		writer.Write(@this.TotalHeaderSize);
		writer.Write(@this.FolderName);
		writer.Write((uint)@this.PackageFlags);


		writer.Write(@this.NameCount);
		writer.Write(@this.NameOffset);


		writer.Write(@this.ExportCount);
		writer.Write(@this.ExportOffset);
		writer.Write(@this.ImportCount);
		writer.Write(@this.ImportOffset);
		writer.Write(@this.DependsOffset);

	}
}