using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseUnit : MonoBehaviour
{
    public string name;
    public enum Team
    {
        One,
        Two,
        Three,
        Four,
        Five,
        Six,
        Seven,
        Eight
    }
    public Team team;
    public int troopCount;
    public int[] casualtyRates;
    public enum UnitType
    {
        Sword,
        Axe,
        Spear,
        Pike,
        Bow,
        Crossbow,
        Javelin,
        Sling,
        Melee_Cavalry,
        Shock_Cavalry,
        Bow_Cavalry,
        Javelin_Cavalry
    }
    public UnitType unitType;
    public enum MeleeRole
    {
        None,
        Light,
        Medium,
        Heavy
    }
    public MeleeRole meleeRole;

    public enum RangedArmour
    {
        None,
        Light,
        Medium
    }
    public RangedArmour rangedArmour;

    public enum Training
    {
        None,
        Decent,
        High
    }
    public Training training;

    [SerializeField] private GameObject gameController;

    public bool isFlankedFromSide = false;
    public bool isFlankedFromBehind = false;
    public bool isAmbushed = false;
    public bool isDemoralized = false;
    public bool isTired = false;
    public bool isUniform = false;
    public bool isInFormation = false;
    public bool isCavalryCharged = false;
    public bool isHighGround = false;
    public bool isBehindWalls = false;

    private float _isFlankedFromSide = 0.2f;
    private float _isFlankedFromBehind = 0.3f;
    private float _isAmbushed = 0.1f;
    private float _isDemoralized = 0.4f;
    private float _isTired = 0.2f;
    private float _isUniform = -0.2f;
    private float _isInFormation = -0.2f;
    private float _isCavalryCharged = 0.3f;
    private float _isHighGround = -0.1f;
    private float _isBehindWalls = -0.3f;

    private void Awake()
    {
        gameController = GameObject.FindGameObjectWithTag("GameController");
    }
    public void SelectEvent()
    {
        Debug.Log("Selected");
        gameController.GetComponent<GameController>().SetActiveUnit(gameObject);
    }
    public void ProcessAttack(int[] attackerCasualtyRates)
    {
        int selectedCasualtyRate = attackerCasualtyRates[UnityEngine.Random.Range(0, attackerCasualtyRates.Length)];

        
        if (isFlankedFromSide)
        {
            Debug.Log("Is flanked from side calc");
            selectedCasualtyRate += Convert.ToInt32(selectedCasualtyRate * _isFlankedFromBehind);
        }
        
        if (isFlankedFromBehind)
        {
            Debug.Log("Is flanked from behind calc");
            selectedCasualtyRate += Convert.ToInt32(selectedCasualtyRate * _isFlankedFromSide);
        }
        
        if (isAmbushed)
        {
            selectedCasualtyRate += Convert.ToInt32(selectedCasualtyRate * _isAmbushed);
        }
        
        if (isDemoralized)
        {
            selectedCasualtyRate += Convert.ToInt32(selectedCasualtyRate * _isDemoralized);
        }
        
        if (isTired)
        {
            selectedCasualtyRate += Convert.ToInt32(selectedCasualtyRate * _isTired);
        }
        
        if (isUniform)
        {
            selectedCasualtyRate += Convert.ToInt32(selectedCasualtyRate * _isUniform);
        }
        
        if (isInFormation)
        {
            selectedCasualtyRate += Convert.ToInt32(selectedCasualtyRate * _isInFormation);
        }
        
        if (isCavalryCharged)
        {
            selectedCasualtyRate += Convert.ToInt32(selectedCasualtyRate * _isCavalryCharged);
        }
        
        if (isHighGround)
        {
            selectedCasualtyRate += Convert.ToInt32(selectedCasualtyRate * _isHighGround);
        }
        
        if (isBehindWalls)
        {
            selectedCasualtyRate += Convert.ToInt32(selectedCasualtyRate * _isBehindWalls);
        }
        

        selectedCasualtyRate = selectedCasualtyRate < 0 ? 0 : selectedCasualtyRate;

        troopCount -= selectedCasualtyRate;
    }
}
