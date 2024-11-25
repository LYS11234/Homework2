using UnityEngine;

using System.Collections.Generic;
using System;

// 상태 변경 알림을 받기 위한 인터페이스 정의
public interface IObserver
{
    void PlayerDead();

    void HPCheck(float _hpPer);

    void StaminaCheck(float _staminaPer);
}

// 옵저버 등록, 해제, 알림을 위한 인터페이스 정의
public interface ISubject
{
    void RegisterObserver(IObserver _observer);
    void RemoveObserver(IObserver _observer);
    void NotifyObservers();
}