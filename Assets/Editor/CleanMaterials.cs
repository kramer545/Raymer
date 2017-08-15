
using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System.IO;
using System.Text;
 
public class CleanMaterials {

    private const string assetFolder = "Assets";

    /// This method selects and returns all selected scenes dependencies.
    public static Object[] SelectScenesDependencies() 
	{
        return EditorUtility.CollectDependencies(Selection.objects);
    }

    /// This method selects and returns all materials inside Assets directory and subdirectories.
	public static Object[] SelectAllMaterials()
	{
        List<Object> allMaterials = new List<Object>();
        string searchPath = string.Empty;

        /// Gets all materials inside "Assets" directory.
        DirectoryInfo dirInfo = new DirectoryInfo(Application.dataPath);
        foreach (FileInfo fi in dirInfo.GetFiles("*.mat"))
        {
            searchPath = fi.DirectoryName.Substring(fi.DirectoryName.IndexOf(assetFolder)) + "\\" + fi.Name;
            Debug.Log(searchPath);
            Debug.Log(AssetDatabase.LoadAssetAtPath(searchPath, typeof(Object)));

            allMaterials.Add(AssetDatabase.LoadAssetAtPath(searchPath, typeof(Object)));
            Debug.Log(fi.Name + " has been added as Material...");
        }

        /// Gets all materials inside all "Assets" subdirectories.
        foreach (string directoryName in Directory.GetDirectories(Application.dataPath,"*", SearchOption.AllDirectories))
        {
            dirInfo = new DirectoryInfo(directoryName);

            switch(dirInfo.Name.ToLower())
            {
                case ".svn":
                case "prop-base":
                case "props":
                case "text-base":
                case "tmp":
                    break;

                default:
                    Debug.Log("We are inside " + dirInfo.Name + " directory...");
                     
                    foreach (FileInfo fi in dirInfo.GetFiles("*.mat"))
                    {
                        searchPath = fi.DirectoryName.Substring(fi.DirectoryName.IndexOf(assetFolder)) + "\\" + fi.Name;
                        allMaterials.Add(AssetDatabase.LoadAssetAtPath(searchPath, typeof(Object)));
                        Debug.Log(fi.Name + " has been added as Material...");
                    }

                    break;
            };
        }
        return allMaterials.ToArray();
    }

    /// This method removes all unused materials.
	public static void RemoveUnusedMaterials(Object[] AllMaterials, Object[] UsedMaterials) 
	{
        Debug.Log("Deleting non-used Materials...");
		Debug.Log("ALLMATSLENGTH: "+ AllMaterials.Length);
        for(uint i = 0; i < AllMaterials.Length; i++)
        {
            Debug.Log("Checking " + AllMaterials[i].name);
			Debug.Log("USEDMATSLENGTH: "+ UsedMaterials.Length);
            for (uint j = 0; j < UsedMaterials.Length; j++)  
            {
				//Debug.Log(UsedMaterials[j].GetType);
                if (UsedMaterials[j].name == AllMaterials[i].name)
                    break;
                else if ((j == UsedMaterials.Length - 1) && (UsedMaterials[j].name != AllMaterials[i].name))
                {
                    Debug.Log(AllMaterials[i].name + " has been deleted...");
                    AssetDatabase.MoveAsset(AssetDatabase.GetAssetPath(AllMaterials[i]),"Assets");
                }
            }
        }
		AssetDatabase.Refresh ();
    } 

    /// The Main method.
	[MenuItem ("Biworld/Clean Materials")]
    public static void CleaningProcess()
    {
        RemoveUnusedMaterials(SelectAllMaterials(), SelectScenesDependencies()); 	
    }
}