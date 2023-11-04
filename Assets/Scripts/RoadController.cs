using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RoadController : MonoBehaviour
{
   [SerializeField] private int _firstRoadCount;
   [SerializeField] private RoadBlock _roadBlockPrefab;
   [SerializeField] private List<RoadBlock> _roadBlockList = new List<RoadBlock>();
   [SerializeField] private Transform _roadBlockParent;
   [SerializeField] private float _poolingDistance;
   private RoadBlock _firstRoadBlock;
   private void Start()
   {
      for (int i = 0; i < _firstRoadCount; i++)
      {
         CreateRoadBlock();
      }
      SetFirstRoadBlock();
   }
   private void Update()
   {
      if (Vector3.Distance(Vector3.zero,_firstRoadBlock.transform.position)>=_poolingDistance)
      {
         ResetRoadBlock(_firstRoadBlock);
      }
   }
   private void ResetRoadBlock(RoadBlock roadBlock)
   {
      roadBlock.gameObject.SetActive(false);
      roadBlock.transform.position = GetLastTargetBlock();
      _roadBlockList.RemoveAt(0);
      _roadBlockList.Add(roadBlock);
      roadBlock.gameObject.SetActive(true);
      SetFirstRoadBlock();
   }
   private void SetFirstRoadBlock()
   {
      _firstRoadBlock = _roadBlockList.First();
   }
   private Vector3 GetLastTargetBlock()
   {
      Vector3 tempTargetPos = _roadBlockList.Last().transform.position;
      tempTargetPos.z += _roadBlockList.Last()._zScale;
      return tempTargetPos;
   }
   private void CreateRoadBlock()
   {
      var tempBlock = Instantiate(_roadBlockPrefab, GetLastTargetBlock(), Quaternion.identity, _roadBlockParent);
       _roadBlockList.Add(tempBlock);
   }

   private List<RoadBlock>[] GetRoadBlocksByIndexes(int firstIndex, int secondIndex)
   {
      var roadBlock = new List<RoadBlock>();
      roadBlock.Add(_firstRoadBlock);
      _roadBlockList.Add(_roadBlockList[_firstRoadBlock]);
   }
   #region Editor
   [ContextMenu("Create Editor Road")]
   public void CreateEditorRoad()
   {
      for (int i = 0; i < _firstRoadCount; i++)
      {
         CreateRoadBlock();
      }
   }
   #endregion
}
