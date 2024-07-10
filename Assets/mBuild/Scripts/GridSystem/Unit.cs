using UnityEngine;
using TMPro;

public class Unit : MonoBehaviour
{
    [field: SerializeField] public int Level { get; private set;}

    [SerializeField] private TextMeshProUGUI _text;

    private SpriteRenderer _spriteRenderer;

    public void Initialize(int level)
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        Level = level;
        UpdateState();
    }

    public void IncreaseLevel()
    {
        Level++;
        UpdateState();
    }

    private void UpdateState()
    {
        _text.text = Level.ToString();
        _spriteRenderer.color = new Color(1, (float)(Level - 1) / 3, 0, 1);
    }
}
