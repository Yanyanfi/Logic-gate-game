using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class CircuitCompiler : MonoBehaviour
{
    [SerializeField] private GridManager gridManager;
    public void Compile()
    {
        if (gridManager != null)
        {
            Debug.LogFormat("not null");
        }
        else
        {
            Debug.LogFormat("is null");
        }
        ClearConnectAndSubscribe();
        Connect();
        AllComponentsSubscribeValues();
        InitCircuit();
    }
    private void Connect()
    {
        Debug.LogFormat("Connect start");
        foreach (var component in gridManager.components)
        {
            foreach (var wire in gridManager.wires)
            {
                if (component.PositionOfInputPin.Intersect(wire.Position).Any())
                {
                    component.inputWires.Add(wire);
                }
                else if (component.PositionOfOutputPin.Intersect(wire.Position).Any())
                {
                    wire.inputComponent.Add(component);
                }
            }
        }
        Debug.LogFormat("Connect finished");
    }
    private void AllComponentsSubscribeValues()
    {
        Debug.LogFormat("SubscribeValues start");
        foreach(var component in gridManager.components)
        {
            if (component.inputWires != null)
            {
                component.SubscribeToInputs();
            }
        }
        foreach(var wire in gridManager.wires)
        {
            if (wire.inputComponent != null)
            {
                wire.SubscribeToInputs();
            }
        }
        Debug.LogFormat("SubscribeValues finished");
    }
    private void InitCircuit()
    {
        Debug.LogFormat("InitCircuit start");
        int count = 0;
        foreach (var wire in gridManager.wires)
        {
            count++;
            wire.OnValueChanged();
        }
        Debug.Log(count);
        foreach (var component in gridManager.components)
        {
            component.OnValueChanged();
        }
        Debug.LogFormat("InitCircuit finished");
    }
    private void ClearConnectAndSubscribe()
    {
        Debug.LogFormat("ClearConnectAndSubscribe start");
        foreach (var wire in gridManager.wires)
        {
            if (wire.inputComponent != null)
            {
                wire.CancelSubscribeToInputs();
                wire.inputComponent.Clear();
                
            }
        }
        foreach (var component in gridManager.components)
        {
            if (component.inputWires != null)
            {
                component.CancelSubscribeToInputs();
                component.inputWires.Clear();
            }
        }
        
        Debug.LogFormat("ClearConnectAndSubscribe finished");
    }
    private void Awake()
    {
    }
    private void Start()
    {
        
    }
    private void Update()
    {

    }
}