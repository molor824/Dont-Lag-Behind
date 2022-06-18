using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelTransition : MonoBehaviour
{
    public int LevelIndex { get => _index; set { _index = value; } }

    [SerializeField] float _transitionSpeed = 1;
    //levels will be "Levels/Level0" "Levels/Level1" ...
    [SerializeField] string _levelPaths = "Levels/Level";

    LevelInfo _nextLevel, _lastLevel, _currentLevel;
    int _transitionDirection = 0;
    float _transition;
    Vector3 _camPos;
    int _index = 0;

    LevelInfo LoadLevel(int index)
    {
        return Resources.Load<LevelInfo>(_levelPaths + index);
    }
    void LoadCurrent()
    {
        _currentLevel = LoadLevel(_index);

        if (_currentLevel) _currentLevel = Instantiate(_currentLevel, null);
    }
    void LoadLast()
    {
        _lastLevel = LoadLevel(_index - 1);

        if (_lastLevel) _lastLevel = Instantiate(_lastLevel, null);
    }
    void LoadNext()
    {
        _nextLevel = LoadLevel(_index + 1);

        if (_nextLevel) _nextLevel = Instantiate(_nextLevel, null);
    }
    void ShiftNext()
    {
        if (_lastLevel) Destroy(_lastLevel.gameObject);

        _lastLevel = _currentLevel;
        _currentLevel = _nextLevel;
        _index++;

        LoadNext();
    }
    void ShiftLast()
    {
        if (_nextLevel) Destroy(_nextLevel.gameObject);

        _nextLevel = _currentLevel;
        _currentLevel = _lastLevel;
        _index--;

        LoadLast();
    }
    void Start()
    {
        //load neighbours
        LoadLast();
        LoadCurrent();
        LoadNext();
    }
    void Update()
    {
        if (
            _currentLevel.NextTrigger
            && _currentLevel.NextTrigger.IsCollided
            && _nextLevel
            && _transitionDirection != 1
        )
        {
            _transitionDirection = 1;
            _camPos = transform.position;
            _transition = 0;
        }
        else if (
            _currentLevel.LastTrigger
            && _currentLevel.LastTrigger.IsCollided
            && _lastLevel
            && _transitionDirection != -1
        )
        {
            _transitionDirection = -1;
            _camPos = transform.position;
            _transition = 0;
        }
        if (_transitionDirection != 0)
        {
            if (_nextLevel?.LastTrigger && _nextLevel.LastTrigger.IsCollided && _transitionDirection != -1)
            {
                // going back during transition

                ShiftNext();
                _transitionDirection = -1;
                _camPos = transform.position;
                _transition = 0;
            }
            if (_lastLevel?.NextTrigger && _lastLevel.NextTrigger.IsCollided && _transitionDirection != 1)
            {
                // going forward during transition

                ShiftLast();
                _transitionDirection = 1;
                _camPos = transform.position;
                _transition = 0;
            }

            _transition += Time.deltaTime * _transitionSpeed;
            if (_transition > 1) _transition = 1;

            var target =
                (_transitionDirection == 1 ? _nextLevel : _lastLevel).transform.position;

            transform.position = Vector3.Lerp(_camPos, target, Ease.OutQuart(_transition));

            if (_transition == 1)
            {
                if (_transitionDirection == 1) ShiftNext();
                else ShiftLast();

                _transitionDirection = 0;
            }
        }
    }
}
