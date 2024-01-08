using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;
using static Circle;

public class CircleSpawner : MonoBehaviour
{
    public delegate void PointsAdded();
    public event PointsAdded OnPointsAdded;
    [SerializeField] private GameObject[] _circles; 
    [SerializeField] private RectTransform _spawnArea; 
    [SerializeField] private RectTransform _canvasRect;
    [SerializeField] private float _circleCheckRadius;
    private float _score = 0;
    private bool _gameOver = false;
    public void SetGameOver() { _gameOver = true; }
    public float GetScore() {  return _score; }
    private void Start()
    {
        StartCoroutine(CirclesSpawner());
        SpawnRandomCircle();
    }
    private void Update()
    {
        if(_gameOver) 
        {
            StopAllCoroutines();
        }
    }

    private void SpawnRandomCircle()
    {
        Vector2 randomPosition = GetRandomPositionInArea(_spawnArea.rect);
        GameObject newCircle = Instantiate(GetRandomElement(), randomPosition, Quaternion.identity);
        newCircle.transform.SetParent(_canvasRect, false);
        Circle _circleScript = newCircle.GetComponent<Circle>();
        _circleScript.OnCircleTaped += AddScorePoints;
    }
    private void AddScorePoints() 
    {
        _score++;
        OnPointsAdded?.Invoke();
    }
    private IEnumerator CirclesSpawner()
    {
        while (true)
        {
            SpawnRandomCircle();

            yield return new WaitForSeconds(0.5f); 
        }
    }

    Vector2 GetRandomPositionInArea(Rect area)
    {
        float randomX = Random.Range(area.xMin, area.xMax);
        float randomY = Random.Range(area.yMin, area.yMax);

        return new Vector2(randomX, randomY);
    }
    private GameObject GetRandomElement()
    {
        int randomIndex = Random.Range(0, _circles.Length);

        return _circles[randomIndex];
    }
}
