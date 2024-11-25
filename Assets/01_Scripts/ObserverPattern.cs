using UnityEngine;

using System.Collections.Generic;
using System;

// ���� ���� �˸��� �ޱ� ���� �������̽� ����
public interface IObserver
{
    void PlayerDead();

    void HPCheck(float _hpPer);

    void StaminaCheck(float _staminaPer);
}

// ������ ���, ����, �˸��� ���� �������̽� ����
public interface ISubject
{
    void RegisterObserver(IObserver _observer);
    void RemoveObserver(IObserver _observer);
    void NotifyObservers();
}