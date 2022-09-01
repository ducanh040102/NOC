using System.Collections;
using UnityEngine;

namespace Player
{
    public class PlayerManager : MonoBehaviour
    {
        public PlayerController player;
        
        public int currentHealth = 5;
        public int maxHealth = 5;
        public int limitHealth = 10;
        
        public bool haveShield;
        public GameObject shield;
        public int shieldStrength = 2;
        public float shieldDuration = 30;

        public int score;
        public int scoreIncrement = 1;
        public float scoreIncrementDelay = 0.1f;

        public bool _gameStart = false;
        public bool isDead = false;
        
        private float _delayTime;
        public float hardMultiply = 1;
        [Tooltip("The next time difficult increase will be score x this value")]
        public float difficultIncrease = 3;
        private float increaseDifficultEach = 1000;

        public static PlayerManager Instance { get; private set; }
        
        private void Awake()
        {
            if (Instance != null && Instance != this) 
            { 
                Destroy(this); 
            } 
            else 
            { 
                Instance = this; 
            }
        }

        private void Start()
        {
            score = 0;
            increaseDifficultEach = 1000;
            isDead = false;
            _delayTime = 0;
            hardMultiply = 1;
        }

        private void Update()
        {
            if (_gameStart)
            {
                _delayTime += Time.deltaTime;

                if (_delayTime > scoreIncrementDelay)
                {
                    score += scoreIncrement;
                    _delayTime = 0;
                }

                if (currentHealth <= 0 || maxHealth <= 0)
                {
                    isDead = true;
                }

                if (score > increaseDifficultEach)
                {
                    hardMultiply += 0.5f;
                    increaseDifficultEach *= difficultIncrease;
                }

                if (isDead)
                {
                    _gameStart = false;
                    player.Dead();
                }
            }
        }

        public void IncreaseHealth(int value)
        {
            if(currentHealth + value <= maxHealth)
                currentHealth += value;
            else
                score += 100;
        }
        
        public void IncreaseMaxHealth(int value)
        {
            if (maxHealth + value <= limitHealth)
                maxHealth += value;
            else
                score += 200;
        }

        public void DecreaseHealth(int value)
        {
            if (haveShield && shieldStrength > 0)
            {
                DecreaseShieldStrength();
            }
            else
            {
                currentHealth -= value;
                player.Hurt();
            }
            
        }
        
        public void DecreaseMaxHealth(int value)
        {
            if (haveShield && shieldStrength > 0)
            {
                DecreaseShieldStrength();
            }
            else
            {
                maxHealth -= value;
                player.Hurt();
            }
        }

        public void DecreaseShieldStrength()
        {
            if (shieldStrength > 1)
            {
                shieldStrength--;
            }
            else if (shieldStrength <= 1)
            {
                BreakShield();
            }
        }

        public void BreakShield()
        {
            haveShield = false;
            shield.SetActive(haveShield);
            shieldStrength = 0;
            shieldDuration = 0;
            StopAllCoroutines();
        }

        public void CreateShield(float duration, int strength)
        {
            shieldDuration = duration;
            shieldStrength = strength;
            
            if (!haveShield)
            {
                haveShield = true;
                shield.SetActive(haveShield);
                StartCoroutine(ShieldTimeout());
            }
            
        }

        IEnumerator ShieldTimeout()
        {
            yield return new WaitForSeconds(shieldDuration);
            BreakShield();
        }
    }
}


