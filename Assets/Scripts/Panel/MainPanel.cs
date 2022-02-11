using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainPanel : MonoBehaviour
{
    [SerializeField] private Panel[] _lineOne;
    [SerializeField] private Panel[] _lineTwo;
    [SerializeField] private GeneratorPoints _generatorPoints;

    private Panel[,] _panels = new Panel[2, 2];
    private bool _peopleWalk = false;
    private int _numberDigitLineOne = 0;
    private int _numberDigitLineTwo = 1;

    private void OnEnable()
    {
        for (int i = 0; i < _panels.GetLength(0); i++)
        {
            for (int j = 0; j < _panels.GetLength(1); j++)
            {
                if (i == 0)
                {
                    AddPanel(_panels, _lineOne, i, j);
                }
                else
                {
                    AddPanel(_panels, _lineTwo, i, j);
                }
            }
        }
        _generatorPoints.Generated += OnMovePeople;
    }

    private void OnDisable()
    {
        _generatorPoints.Generated -= OnMovePeople;
    }

    private void OnMovePeople(bool peopleWalk)
    {
        _peopleWalk = peopleWalk;
    }

    public Vector3 GetPositionPanel(Vector2 direction, int numberX, int numberY)
    {
        int offsetX = 0;
        int offsetY = 0;
        Vector3 target = _panels[numberX, numberY].transform.position;

        if (direction == new Vector2(1, 0))
        {
            offsetX = -1;
        }
        else if (direction == new Vector2(-1, 0))
        {
            offsetX = 1;
        }
        else if (direction == new Vector2(0, 1))
        {
            offsetY = 1;
        }
        else if (direction == new Vector2(0, -1))
        {
            offsetY = -1;
        }

        if (SetTarget(numberX, offsetX, _numberDigitLineOne) && SetTarget(numberY, offsetY, _numberDigitLineTwo))
        {
            if (_panels[numberX + offsetX, numberY + offsetY].FreePanel == true && _peopleWalk == false)
            {
                target = _panels[numberX + offsetX, numberY + offsetY].transform.position;
            }
            else
            {
                target = new Vector3(offsetY, 0, (offsetX * -1));
            }
        }
        else
        {
            target = new Vector3(offsetY, 0, (offsetX * -1));
        }
        return target;
    }

    private void AddPanel(Panel[,] panels, Panel[] line, int lineX, int lineY)
    {
        panels[lineX, lineY] = line[lineY];
        line[lineY].SetNumberPanel(lineX, lineY);
    }

    private bool SetTarget(int number, int offset, int numberDigitArray)
    {
        if ((number + offset) > _panels.GetLength(numberDigitArray) - 1 || (number + offset) < 0)
        {
            return false;
        }
        return true;
    }
}
