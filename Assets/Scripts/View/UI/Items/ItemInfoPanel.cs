using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using View;

public class ItemInfoPanel : ConstructableMonoBehaviour
{
    [SerializeField] private TMP_Text _nameText;
    [SerializeField] private TMP_Text _descriptionText;
    [SerializeField] private Vector3 _offset;

    private GameLibrary _library;

    public override void Construct()
    {
        _library = GameContext.DIContainer.Resolve<GameLibrary>();
    }

    public void Open(string itemId)
    {
        transform.position = Input.mousePosition + _offset;
        gameObject.SetActive(true);
    }

    public void Close()
    {
        gameObject.SetActive(false);
    }
}
