using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragHandler : MonoBehaviour
{
    [SerializeField] private LayerMask _allyUnitsLayer;
    [SerializeField] private LayerMask _gridSlotLayers;

    private Unit _tackedObject;
    private Vector2 _offset;
    private Vector2 _startDragPos;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            var x = Physics2D.OverlapPoint(Camera.main.ScreenToWorldPoint(Input.mousePosition), _allyUnitsLayer);

            if (x == null) return;

            if (x.GetComponent<Unit>())
            {
                _startDragPos = x.transform.position;
                _tackedObject = x.GetComponent<Unit>();
                _offset = _tackedObject.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0));
            }
        }

        if (_tackedObject)
        {
            _tackedObject.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0)) + (Vector3)_offset;
            _tackedObject.transform.position = new Vector3(_tackedObject.transform.position.x, _tackedObject.transform.position.y, -2);
        }

        if (Input.GetKeyUp(KeyCode.Mouse0) && _tackedObject != null)
        {
            var x = Physics2D.OverlapPoint(Camera.main.ScreenToWorldPoint(Input.mousePosition), _gridSlotLayers);

            if (x == null)
            {
                _tackedObject.transform.position = _startDragPos;
                _tackedObject = null;
                return;
            }

            if (x.TryGetComponent(out UnitGridSlot gridSlot))
                gridSlot.UnitGrid.TryTakeSlot(gridSlot, _tackedObject);
            else
                gridSlot.transform.position = _startDragPos;

            _tackedObject.transform.position = new Vector3(_tackedObject.transform.position.x, _tackedObject.transform.position.y, -1);
            _tackedObject = null;
        }
    }
}
