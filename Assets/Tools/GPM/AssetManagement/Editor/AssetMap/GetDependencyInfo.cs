using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace Gpm.AssetManagement
{
    public class GetDependencyInfo
    {
        public GetDependencyInfo(string path)
        {
            System.Type type = AssetDatabase.GetMainAssetTypeAtPath(path);

            if(type == null)
            {
                return;
            }

            if (type.Equals(typeof(UnityEditor.SceneAsset)) == true)
            {
                var sceneRoot = AssetFind.SceneRootManager.GetSceneRoot(path);
                if (sceneRoot == null ||
                    sceneRoot.scene.handle == 0 ||
                    sceneRoot.scene.IsValid() == false ||
                    sceneRoot.scene.isLoaded == false)
                {
                }
                else
                {
                    guid = AssetDatabase.AssetPathToGUID(path);
                    foreach (var obj in sceneRoot.scene.GetRootGameObjects())
                    {
                        FindProperty(obj);
                    }
                }
            }
            else
            {
                Object checkObject = AssetDatabase.LoadMainAssetAtPath(path);
                if (checkObject != null)
                {
                    long localid;
                    if (AssetDatabase.TryGetGUIDAndLocalFileIdentifier(checkObject, out guid, out localid) == true)
                    {
                        FindProperty(checkObject);
                    }
                }
            }
        }

        private string guid;
        private List<int> findObj = new List<int>();

        public List<string> dependencys = new List<string>();
        public List<int> missingInstanceIDs = new List<int>();

        private void FindProperty(Object checkObject)
        {
            if (findObj.Contains(checkObject.GetInstanceID()) == true)
            {
                return;
            }

            findObj.Add(checkObject.GetInstanceID());

            if (checkObject is Transform transform)
            {
                FindProperty(transform.gameObject);

                foreach (Transform child in transform)
                {
                    FindProperty(child);
                }
            }
            else
            {
                SerializedObject serialized_object = new SerializedObject(checkObject);
                SerializedProperty property = serialized_object.GetIterator();

                while (property.Next(true) == true)
                {
                    if (property.propertyType == SerializedPropertyType.ObjectReference)
                    {
                        Object objectReferenceValue = property.objectReferenceValue;
                        if (objectReferenceValue == null)
                        {
                            if (property.objectReferenceInstanceIDValue != 0)
                            {
                                if (missingInstanceIDs.Contains(property.objectReferenceInstanceIDValue) == false)
                                {
                                    missingInstanceIDs.Add(property.objectReferenceInstanceIDValue);
                                }
                            }
                            else
                            {
                                if (property.hasChildren)
                                {
                                    var fileId = property.FindPropertyRelative("m_FileID");
                                    if (fileId != null)
                                    {
                                        if (fileId.intValue != 0)
                                        {
                                            if (missingInstanceIDs.Contains(fileId.intValue) == false)
                                            {
                                                missingInstanceIDs.Add(fileId.intValue);
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        else
                        {
                            if (EditorUtility.IsPersistent(objectReferenceValue) == false)
                            {
                                FindProperty(objectReferenceValue);
                            }
                            else
                            {
                                string guid;
                                long localid;
                                if (AssetDatabase.TryGetGUIDAndLocalFileIdentifier(objectReferenceValue, out guid, out localid) == true)
                                {
                                    if (guid.Equals(this.guid) == true)
                                    {
                                        FindProperty(objectReferenceValue);
                                    }
                                    else
                                    {
                                        if (dependencys.Contains(guid) == false)
                                        {
                                            if (typeof(UnityEditor.MonoScript).IsAssignableFrom(objectReferenceValue.GetType()) == true &&
                                                AssetDatabase.IsMainAsset(objectReferenceValue) == false)
                                            {
                                            }
                                            else if (AssetDatabase.IsMainAsset(objectReferenceValue) == true ||
                                                     AssetDatabase.IsSubAsset(objectReferenceValue) == true)
                                            {
                                                dependencys.Add(guid);
                                            }
                                        }
                                    }
                                }
                            }

                        }
                    }
                }
            }
        }
    }
}