using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections.Generic;
using RPG.Characters;

namespace RPG.CameraUI
{
    public class CameraRaycaster : MonoBehaviour
    {
          
        [SerializeField] Texture2D walkCursor = null;
        [SerializeField] Texture2D targetCursor = null;
        [SerializeField] Vector2 cursorHotspot = new Vector2(0,0); //鼠标坐标

        const int WALKABLE_LAYER = 8;
        float maxRaycastDepth = 100f;

        Rect currentScrenRect;

        public delegate void OnMouseOverTerrain(Vector3 destination); // 声明一个新的委托类型
        public event OnMouseOverTerrain onMouseOverTerrain; // 实例化一个观察者集

        public delegate void OnMouseOverEnemy(EnemyAI enemy); // 声明一个新的委托类型
        public event OnMouseOverEnemy onMouseOverEnemy; // 实例化一个观察者集

        void Update()
        {
            currentScrenRect = new Rect(0, 0, Screen.width, Screen.height);
            // 检查指针是否在可交互的UI元素上
            if (EventSystem.current.IsPointerOverGameObject())
            {
                return; // 停止寻找其他对象 
            }
            else
            {
                PerformRaycasts();
            }
        }

        /*
        * 函数:PerformRaycasts
        * 功能:如果鼠标点击位置在屏幕内，鼠标点击产生射线对象，不同层级不同行为
        * 参数:无
        * 类型:private void
        */
        private void PerformRaycasts()
        {
            if(currentScrenRect.Contains(Input.mousePosition))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (RaycastForEnemy(ray)) { return; }
                if (RaycastForTerrain(ray)) { return; }
            }
        }

        /*
        * 函数:RaycastForEnemy
        * 功能:判断点击的是否是Enemy层级，是改变鼠标图标，通知观察者，不是返回false
        * 参数:Ray ray，鼠标点击位置发出的射线
        * 类型:bool
        */
        bool RaycastForEnemy(Ray ray)
        {
            RaycastHit hitInfo ;
            Physics.Raycast(ray, out hitInfo, maxRaycastDepth);
            var gameObjectHit = hitInfo.collider.gameObject;
            var enemyHit = gameObjectHit.GetComponent<EnemyAI>();
            if(enemyHit)
            {
                Cursor.SetCursor(targetCursor, cursorHotspot, CursorMode.Auto);
                onMouseOverEnemy(enemyHit);
                return true;
            }
            return false;
        }

        /*
        * 函数:RaycastForTerrain
        * 功能:判断点击的是否是Terrain层级，是改变鼠标图标，通知观察者，不是返回false
        * 参数:Ray ray，鼠标点击位置发出的射线
        * 类型:bool
        */
        bool RaycastForTerrain(Ray ray)
        {
            RaycastHit hitInfo;
            LayerMask terrainLayerMask = 1 << WALKABLE_LAYER;
            var terrainHit = Physics.Raycast(ray, out hitInfo, maxRaycastDepth);
            if(terrainHit)
            {
                Cursor.SetCursor(walkCursor, cursorHotspot, CursorMode.Auto);
                onMouseOverTerrain(hitInfo.point);
                return true;
            }
            return false;
        }
    }
}

