using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.PackageManager.UI;
using UnityEngine;

[System.Serializable]
public class Craft
{
    public string _craftName;
    public GameObject _go_Prefab;
    public GameObject _go_PreviewPrefab;
}

public class CraftManual : MonoBehaviour
{
    private bool _isActivated = false;
    private bool _isPreviewAcitvated = false;

    [SerializeField] private GameObject _go_BaseUi;

    [SerializeField] private Craft[] _craft_fire;

    private GameObject _go_Preview;
    private GameObject _go_Prefab;

    [SerializeField] private Transform _tf_Player;

    private RaycastHit _hitInfo;
    [SerializeField] private LayerMask _layerMask;
    [SerializeField] private float _range;

    public void SlotClick(int _slotNumber)
    {
        _go_Preview = Instantiate(_craft_fire[_slotNumber]._go_PreviewPrefab, _tf_Player.position + _tf_Player.forward,
            Quaternion.identity);

        _go_Prefab = _craft_fire[_slotNumber]._go_Prefab;
        _isPreviewAcitvated = true;
        _go_BaseUi.SetActive(false);
    }

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab) && !_isPreviewAcitvated)
        {
            Window();
        }

        if (_isPreviewAcitvated)
        {
            GameManager._isOpenInventory = false;
            PreviewPositionUpdate();
        }


        if (Input.GetButtonDown("Fire1"))
        {
            Build();
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Cancel();
        }
    }

    private void Build()
    {
        if (_isPreviewAcitvated && _go_Preview.GetComponent<PreviewObject>().IsBuildable() )
        {
            Instantiate(_go_Prefab, _hitInfo.point, Quaternion.identity);
            Destroy(_go_Preview);
            _isActivated = false;
            _isPreviewAcitvated = false;
            _go_Preview = null;
            _go_Prefab = null;
        }
    }

    private void PreviewPositionUpdate()
    {
        if (Physics.Raycast(_tf_Player.position, _tf_Player.forward, out _hitInfo, _range, _layerMask))
        {
            if (_hitInfo.transform != null)
            {
                Vector3 _location = _hitInfo.point;
                _go_Preview.transform.position = _location;
            }
        }
    }

    private void Cancel()
    {
        if (_isPreviewAcitvated)
        {
            Destroy(_go_Preview);
        }

        _isActivated = false;
        _isPreviewAcitvated = false;
        _go_Preview = null;

        _go_BaseUi.SetActive(false);
    }

    private void Window()
    {
        if (!_isActivated)
        {
            OpenWindow();
        }
        else
        {
            CloseWindow();
        }
    }

    private void OpenWindow()
    {
        GameManager._isOpenInventory = true;
        _isActivated = true;
        _go_BaseUi.SetActive(true);
    }

    private void CloseWindow()
    {
        _isActivated = false;
        _go_BaseUi.SetActive(false);
    }
}