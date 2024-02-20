using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class PlayerController : MonoBehaviour
{
    public GameObject enemyExplosion;
    public GameObject WorthyWisp;
    [SerializeField] private GameObject slashPrefab;

    [SerializeField] private GameObject dashImage;
    [SerializeField] private GameObject healImage;
    [SerializeField] private GameObject slashImage;

    public int playerLevel = 1;
    public float neededXp = 60;
    [SerializeField] private Slider PlayerXPBar;
    [SerializeField] private TMP_Text PlayerXPText;
    [SerializeField] private TMP_Text PlayerLevelText;

    // defines the ability icons
    [SerializeField] private Sprite dashIcon;
    [SerializeField] private Sprite healIcon;
    [SerializeField] private Sprite slashIcon;

    public bool canHeal = true;
    public bool canDash = true;
    public bool canSlash = true;
    private bool isDashing;
    private bool isHealing;
    private bool isSlashing;
    private float dashingPower = 5f;
    private float dashingTime = 0.2f;
    [SerializeField] private TrailRenderer tr;

    public int tempHats; 

    public bool isClimbing = false;
    public GameObject finalBoss;

    public bool isStaff = false;

    private bool finalBossBool = false;
    public bool isBossFight = false;

    // private bool isOnGround = true;
    private bool crouching = false;
    Rigidbody2D rb;
    private int secondswhendied;
    public AudioSource runningSound;
    public AudioSource SFX;
    public AudioClip heartBreaking;
    public AudioClip itemPickup;
    public string scene;

    public float difficultyScaling;

    public bool AngelPopped = false;
    
    public int coins;

    private float MusicPlayers;

    public TMP_Text HPText;  
    public TMP_Text coinsText; 
    public TMP_Text TimerText;   
    public Slider healthBar;
    public Slider overHealthBar;

    private Animator anim;

    public int magicTrees;
    public int numJumps = 0;
    public float maxHp = 100;
    public float HP = 100;
    public float speed = 10.0f;
    public float jumpHeight = 20.0f;
    public float HpRegen = 0.001f;
    public bool stopRunning = false;
    public bool running = false;
    public bool isSpedUp;
    public bool activateWisp;

    public float damageReduction = 1;

    public int minutes;
    public int seconds;

    public GameObject DamageIndicator;

    // Start is called before the first frame update
    void Start()
    {
    
        levelUpText.gameObject.SetActive(false);
        initialColor = levelUpText.color;
        runningSound.Play(0);
        PlayerXPBar.maxValue = neededXp;
        PlayerLevelText.text = "Level " + playerLevel;
        healthBar.maxValue = 100;
        healthBar.value = 100;
        overHealthBar.maxValue = 100;
        overHealthBar.value = 0;
        coinsText.text = ("Coins: 0");
        HPText.text = ("Health: 100/100");
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        InvokeRepeating("UpdateTimer", 1, 1);

    }
    bool hasRun = false;
    // Update is called once per frame
    void Update()
    {
        if (!PauseMenu.isPaused)
        {
            healthBar.maxValue = (10 * playerLevel + 90 + (GameObject.FindWithTag("ItemManager").GetComponent<ItemsManager>().Apple * 10));
            overHealthBar.maxValue = healthBar.maxValue;

            PlayerXPBar.value = neededXp;
            PlayerXPText.text = ((playerLevel * 45) + 15 - Mathf.Abs(neededXp))+ "/" + ((playerLevel * 45) + 15);
            // checks if the player leveled up
            if (neededXp <= 0) 
            {
                LevelUp();
            }
            
            difficultyScaling = minutes + (seconds/60f);
            anim.SetFloat("runSpeedMultiplier", speed/10f);
            anim.SetFloat("jumpMultiplier", 7f/jumpHeight);

            magicTrees = GameObject.FindWithTag("ItemManager").GetComponent<ItemsManager>().MagicTrees;
            // CHECK FOR IF BOSS BATTLE IS GOING ON
            if (isBossFight && GameObject.FindWithTag("ItemManager").GetComponent<ItemsManager>().MusicPlayerImg.activeSelf  == true && !hasRun)
            {
                MusicPlayers = GameObject.FindWithTag("ItemManager").GetComponent<ItemsManager>().MusicPlayers;
                hasRun = true;
                speed *= (1f + MusicPlayers/3f);
                HpRegen *= (1f + MusicPlayers/2f);
                GetComponent<ShootManager>().bulletDamage *= (0.8f + MusicPlayers/3f);
                GetComponent<ShootManager>().bulletVelocity += (0.8f + MusicPlayers/3f);
                GetComponent<ShootManager>().attackSpeed *= (0.4f + 1f/(MusicPlayers+1f));
            }
            if (hasRun && !isBossFight)
            {
                hasRun = false;
                speed /= (1f + MusicPlayers/3f);
                HpRegen /= (1f + MusicPlayers/2f);
                GetComponent<ShootManager>().bulletDamage /= (0.8f + MusicPlayers/3f);
                GetComponent<ShootManager>().bulletVelocity -= (0.8f + MusicPlayers/3f);
                GetComponent<ShootManager>().attackSpeed /= (0.4f + 1f/(MusicPlayers+1f));
            }
            if (activateWisp)
            {
                WorthyWisp.SetActive(true);
            }
            // THIS IS  FOR THE DETECTION OF THE RED HAT RED WHIP WHATEVER
            if (GameObject.FindWithTag("ItemManager").GetComponent<ItemsManager>().RedHats > 0 && GetComponent<ShootManager>().timeSinceLastShot > 4f && isSpedUp == false)
            {
                isSpedUp = true;
                tempHats = GameObject.FindWithTag("ItemManager").GetComponent<ItemsManager>().RedHats;
                speed *= (1f + tempHats * 0.4f);
            }
            if (HP <= (10 * playerLevel + 90 + (GameObject.FindWithTag("ItemManager").GetComponent<ItemsManager>().Apple * 10)))
            {
                healthBar.value = HP;
                overHealthBar.value = 0;
            }
            if (HP > (10 * playerLevel + 90 + (GameObject.FindWithTag("ItemManager").GetComponent<ItemsManager>().Apple * 10)) && HP <= (20 * playerLevel + 180 + (GameObject.FindWithTag("ItemManager").GetComponent<ItemsManager>().Apple * 20)))
            {
                healthBar.value = HP;
                overHealthBar.value = (HP-(10 * playerLevel + 90 +(GameObject.FindWithTag("ItemManager").GetComponent<ItemsManager>().Apple * 10)));
            }
            coinsText.text = ("Coins: " + coins);
            HPText.text = ("Health: " + Mathf.RoundToInt(HP) + "/" + (10 * playerLevel + 90 + (GameObject.FindWithTag("ItemManager").GetComponent<ItemsManager>().Apple * 10)));
            if (isDashing || isHealing)
            {
                return;
            }
            if (!anim.GetBool("dead"))
            {
                // DELETE THIS LATER PLZ
                if (Input.GetKeyDown("p") && finalBossBool == false)
                {
                    finalBossBool = true;
                    // CHECKS PLAYER POS
                    if (transform.position.x - 60 < -230)
                    {
                        Instantiate(finalBoss, new Vector3(transform.position.x + 40, transform.position.y + 10, transform.position.z), Quaternion.identity);
                    }
                    if (transform.position.x + 60 > 230)
                    {
                        Instantiate(finalBoss, new Vector3(transform.position.x - 40, transform.position.y + 10, transform.position.z), Quaternion.identity);
                    }
                    if (transform.position.x < 170 && transform.position.x > -170)
                    {
                        if (Random.Range(0, 2) == 0) 
                        {
                            Instantiate(finalBoss, new Vector3(Random.Range(transform.position.x-60, transform.position.x-20), 0, 0), Quaternion.identity);
                        } else 
                        {
                            Instantiate(finalBoss, new Vector3(Random.Range(transform.position.x+20, transform.position.x+60), 0, 0), Quaternion.identity);
                        }               
                    }
                }

                // slash ability
                if (Input.GetKeyDown(KeyCode.Mouse1) && canSlash && playerLevel >= 2)
                {
                    canSlash = false;
                    StartCoroutine(Slash());
                }
                // dash ability
                if (Input.GetKeyDown("c") && canDash && playerLevel >= 3)
                {
                    canDash = false;
                    StartCoroutine(Dash());
                }
                // heal ability
                if (Input.GetKeyDown("v") && canHeal && isOnGround() && playerLevel >= 5)
                {
                    canHeal = false;
                    StartCoroutine(Heal());
                }
                // // DUCK DETECTION (not the animal)
                // if (Input.GetKeyDown("s") && isOnGround && !crouching)
                // {
                //     crouching = true;
                //     transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y/2f, transform.localScale.z);
                // }
                if (Input.GetKeyUp("s") && crouching)
                {
                    crouching = false;
                    transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y * 2f, transform.localScale.z);
                }
                if (Input.GetKeyDown("space") && (isOnGround() || numJumps >= 1)) {
                    Debug.Log(numJumps);
                    rb.velocity = Vector2.up * jumpHeight;
                    stopRunning = true;
                    if (!isOnGround())
                    {
                        numJumps -= 1;
                    }
                    

                    anim.Play("JumpAnimation", -1, 0f);

                }
                if (Input.GetAxisRaw("Horizontal") > 0 && !GetComponent<ShootManager>().isShooting) 
                {
                    transform.localScale = new Vector3(6.5f, transform.localScale.y, 6.5f);



                }
                if (Input.GetAxisRaw("Horizontal") < 0 && !GetComponent<ShootManager>().isShooting)
                {
                    transform.localScale = new Vector3(-6.5f, transform.localScale.y, 6.5f);

                    
                }
                if (Input.GetKey("a") || Input.GetKey("left") || Input.GetKey("d") || Input.GetKey("right"))
                {
                        anim.SetBool("running", true);
                        running = true;
                    
                }
                
                if (Input.GetAxisRaw("Horizontal") == 0 || stopRunning == true)
                {   
                    anim.SetBool("running", false);
                    running = false;
                }

                if (transform.localScale.x != 0)
                {
                    if (HP <= (maxHp * 0.3f) && GameObject.FindWithTag("ItemManager").GetComponent<ItemsManager>().EnergyDrinks > 0)
                    {
                        transform.Translate(Vector2.right * Time.deltaTime * (speed * (1f + GameObject.FindWithTag("ItemManager").GetComponent<ItemsManager>().EnergyDrinks/5f)) * Input.GetAxisRaw("Horizontal"));
            
                    }
                    if (crouching)
                    {
                        transform.Translate(Vector2.right * Time.deltaTime * (speed/2f) * Input.GetAxisRaw("Horizontal"));

                    }
                    else 
                    {
                        transform.Translate(Vector2.right * Time.deltaTime * speed * Input.GetAxisRaw("Horizontal"));
                    }
                }
                
                
                if (running == true && StatsManager.doSFX == true)
                {
                    runningSound.volume = (StatsManager.Volume/50f);

                } 
                if (running == false && isOnGround())
                {
                    // EXTRA HEALING AND RUNNING SOUND
                    runningSound.volume = 0f;
                    if (GameObject.FindWithTag("ItemManager").GetComponent<ItemsManager>().Medkits > 0)
                    {
                        HP += HpRegen * (1 + (GameObject.FindWithTag("ItemManager").GetComponent<ItemsManager>().Medkits)/2f);
                    }
                }
            }
            // external hearts
            if (HP <= (maxHp * 0.15f) && GameObject.FindWithTag("ItemManager").GetComponent<ItemsManager>().InternalHearts > 0)
            {
                HP += (maxHp * 0.3f);
                GameObject.FindWithTag("ItemManager").GetComponent<ItemsManager>().InternalHearts -= 1;
                GameObject.FindWithTag("ItemManager").GetComponent<ItemsManager>().InternalHeartText.text = (GameObject.FindWithTag("ItemManager").GetComponent<ItemsManager>().InternalHearts.ToString()); 
                DamageIndicator.GetComponent<FloatingMessage>().color = Color.green;
                DamageIndicator.GetComponent<FloatingMessage>().damage = (maxHp * 0.3f);
                if (StatsManager.doSFX == true)
                {
                    SFX.pitch = Random.Range(0.8f, 1.1f);
                    SFX.PlayOneShot(heartBreaking, (StatsManager.Volume/85f));
                }
            
        
                // spawns the damage text
                Instantiate(DamageIndicator, transform.position, Quaternion.identity);  
                
            }
            // health checks and stuff
            if (HP <= 0) {
                if ((GameObject.FindWithTag("ItemManager").GetComponent<ItemsManager>().Angels + 1) > 1)
                {
                    GameObject.FindWithTag("ItemManager").GetComponent<ItemsManager>().Angels -= 1;
                    GameObject.FindWithTag("ItemManager").GetComponent<ItemsManager>().AngelText.text = (GameObject.FindWithTag("ItemManager").GetComponent<ItemsManager>().Angels.ToString()); 
                    DamageIndicator.GetComponent<FloatingMessage>().color = Color.green;
                    DamageIndicator.GetComponent<FloatingMessage>().damage = maxHp;
                    // SFX.pitch = Random.Range(0.8f, 1.1f);
                    // SFX.PlayOneShot(heartBreaking, 0.6f);
            
                    // spawns the damage text
                    Instantiate(DamageIndicator, transform.position, Quaternion.identity);  
                    HP = maxHp;
                    AngelPopped = true;
                    Instantiate(enemyExplosion, new Vector3(transform.position.x, (transform.position.y + 5f), 0), Quaternion.identity);
                }
                else if ((GameObject.FindWithTag("ItemManager").GetComponent<ItemsManager>().Angels + 1) <= 1)
                {
                    anim.SetBool("dead", true);
                    StartCoroutine(sendToAbyss());
                    
                }
            }
            if (!anim.GetBool("dead"))
            {
                if (HP + HpRegen <= maxHp && magicTrees == 0) 
                {
                    HP += HpRegen;
                }
                if (HP + HpRegen > maxHp && magicTrees == 0)
                {
                    HP = maxHp;
                }
                if (HP + HpRegen <= (maxHp* (1 + magicTrees)) && magicTrees > 0)
                {
                    HP += HpRegen;
                }
                if (HP + HpRegen > (maxHp* (1 + magicTrees)) && magicTrees > 0)
                {
                    HP = (maxHp* (1 + magicTrees));
                }
            }
            
            // Debug.Log("HP " + HP + " Regen " + HpRegen + " Max " + maxHp);

            // CHECKS IF YOU GO OUT OF THE MAP
            if (transform.position.x < -231f)
            {
                transform.position = new Vector3(-230f, -2.491606f, 0);
            }
            if (transform.position.x > 231f)
            {
                transform.position = new Vector3(230f, -1.871606f, 0);
            }
            if (transform.position.y < -4)
            {
                transform.position = new Vector3(transform.position.x, -1.5f, 0);
                rb.velocity = Vector2.up * 0;

            }
        }
        
    }
    // DEATH 3 SECONDS TIMER
    IEnumerator sendToAbyss()
    {
        if (seconds < 10)
        {
            EndgameManager.time = minutes + ":0" + seconds;
        }
        if (seconds >= 10)
        {
            EndgameManager.time = minutes + ":" + seconds;
        }
        EndgameManager.level = playerLevel;
        yield return new WaitForSeconds(3);
        invokeScene("StatsMenu");
    }
    // fade out 
    public void invokeScene(string sceneName)
    {
        scene = sceneName;
        InvokeRepeating("LoadScene", 0, 0.01f);
    }
    public void LoadScene()
    {
        // if (GameObject.FindWithTag("fade").GetComponent<Transform>().localScale.x < 1000f)
        // {
        if (GameObject.FindWithTag("fade").GetComponent<Transform>().localScale.x < 6)
        {
            // GameObject.FindWithTag("fade").transform.localScale.x += 0.1f;
            GameObject.FindWithTag("fade").GetComponent<Transform>().localScale += new Vector3(0.005f, 0.005f, 0);
        }
        else
        {
            SceneManager.LoadScene(scene);

        }
    }
    public void UpdateTimer()
    {
        
        seconds++;
        if (seconds >= 60)
        {
            seconds = 0;
            minutes++;
        }
        if (seconds < 10)
        {
            TimerText.text = (minutes + ":0" + seconds).ToString();
        }
        if (seconds >= 10)
        {
            TimerText.text = (minutes + ":" + seconds).ToString();
        }
        if ((minutes % 5) == 0 && minutes > 1 && seconds == 0)
        {
            GameObject.FindWithTag("SpawnManager").GetComponent<SpawnManager>().SpawnBoss();
        }
    }
    // for the level up text
    public TextMeshProUGUI levelUpText;
    private float fadeDuration = 2f;
    private float displayDuration = 2f;
    private float floatSpeed = 1f;

    private float displayTimer;
    private Color initialColor;

    IEnumerator FadeOut()
    {
        while (displayTimer > 0)
        {
            displayTimer -= Time.deltaTime;
            float alpha = Mathf.Lerp(1f, 0f, (displayDuration - displayTimer) / fadeDuration);
            levelUpText.color = new Color(initialColor.r, initialColor.g, initialColor.b, alpha);
            levelUpText.transform.position += Vector3.up * floatSpeed * Time.deltaTime;
            yield return null;
        }

        levelUpText.gameObject.SetActive(false);
    }
    void OnCollisionEnter2D(Collision2D other) {
        // cancelles the jump animation (took way too long)
        if (other.gameObject.tag == "Ground") {
            if (anim.GetCurrentAnimatorStateInfo(0).IsName("JumpAnimation"))
            {
                anim.SetTrigger("grounded");
            }
            
            stopRunning = false;
            // anim.SetBool("jumping", false);
            numJumps = (GameObject.FindWithTag("ItemManager").GetComponent<ItemsManager>().FlyingShoes);
            
        }
        
    }
    public LayerMask groundLayer;
    public Vector2 boxSize;
    public float castDistance;
    // checks if the player is grounded 
    private bool isOnGround()
    {
        if (Physics2D.BoxCast(new Vector3(transform.position.x-0.3f, transform.position.y, transform.position.z), boxSize, 0, -transform.up, castDistance, groundLayer))
        {
            return true;
        }
        else
        {
            return false;
        }
    }   
    // draws the box that checks for isgrounded
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(new Vector3(transform.position.x-0.3f, transform.position.y, transform.position.z)-transform.up * castDistance, boxSize);
    }
    // ROPE CLIMBING
    void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.tag == "rope")
        {
            if (Input.GetKey("w"))
            {
                isClimbing = true;
                GetComponent<Rigidbody2D>().gravityScale = 0; //to asign
                GetComponent<Rigidbody2D>().velocity = Vector3.zero;
                transform.position += new Vector3(0, 0.25f, 0); 
            }
            if (!Input.GetKey("w") && isClimbing)
            {
                GetComponent<Rigidbody2D>().gravityScale = 2.5f; //to asign

            }
        }
    }
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "rope")
        {
            isClimbing = false;
            GetComponent<Rigidbody2D>().gravityScale = 5; //to asign

        }
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Item" && StatsManager.doSFX == true)
        {
            SFX.pitch = Random.Range(0.8f, 1.1f);
            SFX.PlayOneShot(itemPickup, (StatsManager.Volume/50f));
        }
    }
    private IEnumerator Slash()
    {
        GameObject.FindWithTag("CooldownManager").GetComponent<SlashCooldownManager>().ResetSlashCooldown();
        GameObject slash = Instantiate(slashPrefab, this.transform);
        yield return new WaitForSeconds(0.5f);
        Destroy(slash);
    }
    private IEnumerator Dash()
    {
        GameObject.FindWithTag("CooldownManager").GetComponent<DashCooldownManager>().ResetDashCooldown();
        isDashing = true;
        rb.gravityScale = 0;
        rb.velocity += new Vector2(transform.localScale.x * dashingPower, 0f);
        tr.emitting = true;
        yield return new WaitForSeconds(dashingTime);
        tr.emitting = false;
        rb.gravityScale = 5;
        isDashing = false;
    }
    private IEnumerator Heal()
    {
        GameObject.FindWithTag("CooldownManager").GetComponent<HealCooldownScript>().ResetHealCooldown();
        isHealing = true;
        // animation
        anim.SetTrigger("healing");
        yield return new WaitForSeconds(0.3f);
        if ((HP + (maxHp/14f)) < maxHp)
        {
            HP += (maxHp/14f);
        }
        else 
        {
            HP = maxHp;
        }
        // healing text
        DamageIndicator.GetComponent<FloatingMessage>().color = Color.green;
        DamageIndicator.GetComponent<FloatingMessage>().damage = (maxHp/14f);
        // spawns the damage text
        Instantiate(DamageIndicator, transform.position, Quaternion.identity);
        yield return new WaitForSeconds(0.2f);
        isHealing = false; 
    }

    public void LevelUp()
    {
        maxHp += 10;
        HP += 10;
        playerLevel += 1;
        neededXp = (playerLevel * 45) + 15 - Mathf.Abs(neededXp);
        PlayerXPBar.maxValue = neededXp;
        PlayerLevelText.text = "Level " + playerLevel;
        if (playerLevel == 2)
        {
            slashImage.GetComponent<Image>().sprite = slashIcon;
        }
        if (playerLevel == 3)
        {
            dashImage.GetComponent<Image>().sprite = dashIcon;

        }
        if (playerLevel == 5)
        {
            healImage.GetComponent<Image>().sprite = healIcon;

        }
        levelUpText.gameObject.SetActive(true);
        levelUpText.color = initialColor;
        levelUpText.text = "LEVEL UP";

        displayTimer = displayDuration;
        StartCoroutine(FadeOut());
    }
}
