using Infrastructure;
using UnityEngine;

public class DragHandler 
{
    private Unit _takedObject;
    private Vector2 _startDragPos;
    private Vector2 _offset;

    private Game _game;

    private Vector3 MousePosition => Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10));

    public DragHandler(Game game) => _game = game;

    public void Operate()
    {
        TakeUnit();
        MoveUnit();
        CheckDrop();
    }

    private void TakeUnit()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            var colliders = Physics2D.OverlapPointAll(MousePosition);

            foreach (Collider2D collider in colliders)
            {
                if (collider.TryGetComponent(out Unit unit))
                {
                    if (!unit.GetComponent<DragableObject>()) return;

                    _takedObject = unit;
                    _startDragPos = unit.transform.position;
                    _offset = _takedObject.transform.position - MousePosition;
                }
            }
        }
    }

    private void MoveUnit()
    {
        if (_takedObject)
        {
            _takedObject.transform.position = MousePosition + (Vector3)_offset;
            _takedObject.transform.position = new Vector3(_takedObject.transform.position.x, _takedObject.transform.position.y, -2);
        }
    }

    private void CheckDrop()
    {
        if (Input.GetKeyUp(KeyCode.Mouse0) && _takedObject != null)
        {
            Collider2D[] colliders = Physics2D.OverlapPointAll(MousePosition);

            foreach (Collider2D collider in colliders)
            {
                if (collider.TryGetComponent(out AllyGridSlot gridSlot))
                {
                    _game.AllyUnitGrid.TryTakeSlot(gridSlot, _takedObject);
                    _takedObject = null;
                    return;
                }
            }

            _takedObject.transform.position = _startDragPos;
            _takedObject = null;
        }
    }
}
