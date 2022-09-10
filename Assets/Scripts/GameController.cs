using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameController : MonoBehaviour
{
    public TMP_Text name;
    public TMP_Text unitNum;
    public GameObject floatingUi;
    public Toggle isFlankedFromSide;
    public Toggle isFlankedFromBehind;
    public Toggle isAmbushed;
    public Toggle isDemoralized;
    public Toggle isTired;
    public Toggle isUniform;
    public Toggle isInFormation;
    public Toggle isCavalryCharged;
    public Toggle isHighGround;
    public Toggle isBehindWalls;
    public TMP_Text selectTarget;

    private GameObject[] allUnits;
    private GameObject activeUnit;
    private Camera mainCamera;
    private BaseUnit hitUnit;
    private BaseUnit activeUnitScript;

    private bool _isInAttackMode = false;
    // Start is called before the first frame update
    private void Awake()
    {
        floatingUi.SetActive(false);
        selectTarget.enabled = false;
        allUnits = GameObject.FindGameObjectsWithTag("Unit");
        mainCamera = Camera.main;
        isFlankedFromSide.onValueChanged.AddListener(delegate {
            IsFlankedFromSide_onChanged();
        });
        isFlankedFromBehind.onValueChanged.AddListener(delegate {
            isFlankedFromBehind_onChanged();
        });
        isAmbushed.onValueChanged.AddListener(delegate {
            isAmbushed_onChanged();
        });
        isDemoralized.onValueChanged.AddListener(delegate {
            isDemoralized_onChanged();
        });
        isTired.onValueChanged.AddListener(delegate {
            isTired_onChanged();
        });
        isUniform.onValueChanged.AddListener(delegate {
            isUniform_onChanged();
        });
        isInFormation.onValueChanged.AddListener(delegate {
            isInFormation_onChanged();
        });
        isCavalryCharged.onValueChanged.AddListener(delegate {
            isCavalryCharged_onChanged();
        });
        isHighGround.onValueChanged.AddListener(delegate {
            isHighGround_onChanged();
        });
        isBehindWalls.onValueChanged.AddListener(delegate {
            isBehindWalls_onChanged();
        });
    }

    // Update is called once per frame
    void Update()
    {
        //CheckDeselectUnit();
        CheckForAttackOrder();
    }
    public void SetActiveUnit(GameObject selectedUnit)
    {
        floatingUi.SetActive(true);
        activeUnit = selectedUnit;
        activeUnitScript =  activeUnit.GetComponent<BaseUnit>();
        GetModifierValues();
        name.SetText(selectedUnit.GetComponent<BaseUnit>().name);
        unitNum.SetText(selectedUnit.GetComponent<BaseUnit>().troopCount.ToString());
    }
    void IsFlankedFromSide_onChanged()
    {
        activeUnitScript.isFlankedFromSide = isFlankedFromSide.isOn;
    }
    void isFlankedFromBehind_onChanged()
    {
        activeUnitScript.isFlankedFromBehind = isFlankedFromBehind.isOn;
    }
    void isAmbushed_onChanged()
    {
        activeUnitScript.isAmbushed = isAmbushed.isOn;
    }
    void isDemoralized_onChanged()
    {
        activeUnitScript.isDemoralized = isDemoralized.isOn;
    }
    void isTired_onChanged()
    {
        activeUnitScript.isTired = isTired.isOn;
    }
    void isUniform_onChanged()
    {
        activeUnitScript.isUniform = isUniform.isOn;
    }
    void isInFormation_onChanged()
    {
        activeUnitScript.isInFormation = isInFormation.isOn;
    }
    void isCavalryCharged_onChanged()
    {
        activeUnitScript.isCavalryCharged = isCavalryCharged.isOn;
    }
    void isHighGround_onChanged()
    {
        activeUnitScript.isHighGround = isHighGround.isOn;
    }
    void isBehindWalls_onChanged()
    {
        activeUnitScript.isBehindWalls = isBehindWalls.isOn;
    }
    private void GetModifierValues()
    {

        isFlankedFromSide.isOn = activeUnitScript.isFlankedFromSide;
        isFlankedFromBehind.isOn = activeUnitScript.isFlankedFromBehind;
        isAmbushed.isOn = activeUnitScript.isAmbushed;
        isDemoralized.isOn = activeUnitScript.isDemoralized;
        isTired.isOn = activeUnitScript.isTired;
        isUniform.isOn = activeUnitScript.isUniform;
        isInFormation.isOn = activeUnitScript.isInFormation;
        isCavalryCharged.isOn = activeUnitScript.isCavalryCharged;
        isHighGround.isOn = activeUnitScript.isHighGround;
        isBehindWalls.isOn = activeUnitScript.isBehindWalls;
    }
    private void CheckDeselectUnit()
    {
        floatingUi.SetActive(false);
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
            if (hit.collider == null && activeUnit != null && !_isInAttackMode)
            {
                activeUnit = null;
                name.SetText("");
                unitNum.SetText("");
            }
        }
    }


    private void CheckForAttackOrder()
    {
        if (Input.GetMouseButtonDown(1) && activeUnit != null)
        {
            Debug.Log("Clicking in Attack mode");
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
            if (hit.collider != null)
            {
                hitUnit = hit.collider.gameObject.GetComponent<BaseUnit>();
                if(hit.collider.gameObject != activeUnit && hitUnit.team != activeUnitScript.team)
                {
                    selectTarget.text = "Attacking " + hitUnit.name;
                    hitUnit.ProcessAttack(activeUnitScript.casualtyRates);
                }
            }
        }
    }
}
