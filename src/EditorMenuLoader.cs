//-----------------------------------------------------------------------------
//
//      Terrain loader for Unity main menu (See "Tools -> Load GeoData) 
//      (c) maisvendoo 22/05/2017
//
//-----------------------------------------------------------------------------
using UnityEditor;
using UnityEngine;

namespace GeoData
{
    public class EditorMenuLoader
    {
        [MenuItem("Tools/Load GeoData")]
        static void LoadGeoData()
        {
            // Path to geo data header file selection
            string path = EditorUtility.OpenFilePanel("Data file header", "", "hdr");            

            // Get selected object
            GameObject gameObject = Selection.activeGameObject;                        

            // Get terrain from selected object 
            Terrain terrain = new Terrain();            

            terrain = gameObject.GetComponent<Terrain>();           

            // Check that selected object is terrain
            if (terrain == null)
            {
                Debug.LogWarning("Selected object " + gameObject.name + " is't Terrain. Please, select Terrain ");
                return;
            }                        

            // Loading of terrain data
            GeoDataLoader loader = new GeoDataLoader();

            if (!loader.load(path, terrain.terrainData.heightmapResolution))
            {
                Debug.LogError("Geo data loading is failed");
                return;
            }
            else
            {
                Debug.Log("Geo data loaded successfully");

                // Set terrain size
                terrain.terrainData.size = new Vector3(loader.terrainHeightData.x_range,
                                                       loader.terrainHeightData.y_range,
                                                       loader.terrainHeightData.z_range);

                // Set heightmap data
                terrain.terrainData.SetHeights(0, 0, loader.heightMap);
            }
        }
    }
}
