using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ItemsManager : MonoBehaviour
{
    // DESCRIPTION TEXT
    public TMP_Text ItemInfoText;

    // variables
    private int order = 0;
    private float textCoord = -693f;
    private float imgCoord = -640;

    // HOW MANY OF ITEMS THERE ARE
    public int AngelCount = 0;
    public int HeartsCount = 0;

    public int Coffee = 0;
    public int Apple = 0;
    public int SharpBullets = 0;
    public int BigBullets = 0;
    public int Spellbooks = 0;
    public int FlyingShoes = 0;
    public int Sacrafices = 0;
    public int FrostSpells = 0;
    public int Forcefields = 0;
    public int Acid = 0;
    public int MagicRings = 0;
    public int Bombs = 0;
    public int FocusCrystals = 0;
    public int Medkits = 0;
    public int EnergyDrinks = 0;
    public int MagicTrees = 0;
    public int InternalHearts = 0;
    public int Cauldrons = 0;
    public int StunGuns = 0;
    public int Mentors = 0;
    public int Angels = 0;
    public int Magnets = 0;
    public int Shields = 0;
    public int Vortexes = 0;
    public int Stopwatches = 0;
    public int Staffs = 0;
    public int Wreaths = 0;
    public int Springers = 0;
    public int RedHats = 0;
    public int Wisps = 0;
    public int MusicPlayers = 0;
    public int LightningHammers = 0;
    public int HotPeppers = 0;
    public int KnowledgeCrystals = 0;



    // RAWIMAGES
    public GameObject CoffeeImg;
    public GameObject AppleImg;
    public GameObject SharpBulletsImg;
    public GameObject BigBulletsImg;
    public GameObject SpellbookImg;
    public GameObject FlyingShoesImg;
    public GameObject SacraficeImg;
    public GameObject FrostSpellImg;
    public GameObject ForceFieldImg;
    public GameObject AcidImg;
    public GameObject MagicRingImg;
    public GameObject BombImg;
    public GameObject FocusCrystalImg;
    public GameObject MedkitImg;
    public GameObject EnergyDrinkImg;
    public GameObject MagicTreeImg;
    public GameObject InternalHeartImg;
    public GameObject CauldronImg;
    public GameObject StunGunImg;
    public GameObject MentorImg;
    public GameObject AngelImg;
    public GameObject MagnetImg;
    public GameObject ShieldImg;
    public GameObject VortexImg;
    public GameObject StopwatchImg;
    public GameObject StaffImg;
    public GameObject WreathImg;
    public GameObject SpringImg;
    public GameObject RedHatImg;
    public GameObject WispImg;
    public GameObject MusicPlayerImg;
    public GameObject LightningHammerImg;
    public GameObject HotPepperImg;
    public GameObject KnowledgeCrystalImg;

    // TEXTS
    public TMP_Text coffeeText;  
    public TMP_Text appleText;  
    public TMP_Text wandText;  
    public TMP_Text mushroomText;  
    public TMP_Text spellbookText;  
    public TMP_Text shoesText; 
    public TMP_Text sacraficeText; 
    public TMP_Text FrostSpellText;
    public TMP_Text ForceFieldText;
    public TMP_Text AcidText;
    public TMP_Text MagicRingText;
    public TMP_Text BombText;
    public TMP_Text FocusCrystalText;
    public TMP_Text MedkitText;
    public TMP_Text EnergyDrinkText;
    public TMP_Text MagicTreeText;
    public TMP_Text InternalHeartText;
    public TMP_Text CauldronText;
    public TMP_Text StunGunText;
    public TMP_Text MentorText;
    public TMP_Text AngelText;
    public TMP_Text MagnetText;
    public TMP_Text ShieldText;
    public TMP_Text VortexText;
    public TMP_Text StopwatchText;
    public TMP_Text StaffText;
    public TMP_Text WreathText;
    public TMP_Text SpringText;
    public TMP_Text RedHatText;
    public TMP_Text WispText;
    public TMP_Text MusicPlayerText;
    public TMP_Text LightningHammerText;
    public TMP_Text HotPepperText;
    public TMP_Text KnowledgeCrystalText;


    // Start is called before the first frame update
    void Start()
    {
        CoffeeImg.SetActive(false);
        AppleImg.SetActive(false);
        SharpBulletsImg.SetActive(false);
        BigBulletsImg.SetActive(false);
        SpellbookImg.SetActive(false);
        FlyingShoesImg.SetActive(false);
        SacraficeImg.SetActive(false);
        FrostSpellImg.SetActive(false);
        ForceFieldImg.SetActive(false);
        AcidImg.SetActive(false);
        MagicRingImg.SetActive(false);
        BombImg.SetActive(false);
        FocusCrystalImg.SetActive(false);
        MedkitImg.SetActive(false);
        EnergyDrinkImg.SetActive(false);
        MagicTreeImg.SetActive(false);
        InternalHeartImg.SetActive(false);
        CauldronImg.SetActive(false);
        StunGunImg.SetActive(false);
        MentorImg.SetActive(false);
        AngelImg.SetActive(false);
        MagnetImg.SetActive(false);
        ShieldImg.SetActive(false);
        VortexImg.SetActive(false);
        StopwatchImg.SetActive(false);
        StaffImg.SetActive(false);
        WreathImg.SetActive(false);
        SpringImg.SetActive(false);
        RedHatImg.SetActive(false);
        WispImg.SetActive(false);
        MusicPlayerImg.SetActive(false);
        LightningHammerImg.SetActive(false);
        HotPepperImg.SetActive(false);
        KnowledgeCrystalImg.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {
        // UPDATES STATS
        EndgameManager.items = Coffee + Apple + SharpBullets + AngelCount + Mentors + StunGuns + Cauldrons + BigBullets 
    + HeartsCount + MagicTrees + EnergyDrinks + Spellbooks + FlyingShoes + Sacrafices 
    + FrostSpells + Forcefields + Acid + MagicRings + Bombs + FocusCrystals + Medkits + Magnets + Shields + Vortexes + Stopwatches
     + Staffs + Wreaths + Springers + RedHats + Wisps + MusicPlayers + LightningHammers + HotPeppers + KnowledgeCrystals; 
        // CHECKS ORDER for the thingy so it doesnt go out of screen
        if (order >= 28)
        {
            order = 0;
            imgCoord -= 90f;
            textCoord -= 90f;
        }
        if (Coffee > 0) 
        {
            if(CoffeeImg.activeSelf == false)
            {
                CoffeeImg.GetComponent<RectTransform>().anchoredPosition = new Vector3((70 + (order*70)), imgCoord, 0);
                coffeeText.GetComponent<RectTransform>().anchoredPosition = new Vector3((70 + (order*70)), textCoord, 0);
                CoffeeImg.SetActive(true);
                order += 1;
            }
            
            coffeeText.text = (Coffee.ToString());
        }
        if (Apple > 0)
        {
            if (AppleImg.activeSelf == false)
            {
                AppleImg.GetComponent<RectTransform>().anchoredPosition = new Vector3((70 + (order*70)), imgCoord, 0);
                appleText.GetComponent<RectTransform>().anchoredPosition = new Vector3((70 + (order*70)), textCoord, 0);
                AppleImg.SetActive(true);
                order += 1;
            }
            
            appleText.text = (Apple.ToString());

        }
        if (SharpBullets > 0) 
        {
            if (SharpBulletsImg.activeSelf == false)
            {
                SharpBulletsImg.GetComponent<RectTransform>().anchoredPosition = new Vector3((70 + (order*70)), imgCoord, 0);
                wandText.GetComponent<RectTransform>().anchoredPosition = new Vector3((70 + (order*70)), textCoord, 0);
                SharpBulletsImg.SetActive(true);
                order += 1;
            }
            
            wandText.text = (SharpBullets.ToString());
        }
        if (BigBullets > 0)
        {
            if (BigBulletsImg.activeSelf == false)
            {
                BigBulletsImg.GetComponent<RectTransform>().anchoredPosition = new Vector3((70 + (order*70)), imgCoord, 0);
                mushroomText.GetComponent<RectTransform>().anchoredPosition = new Vector3((70 + (order*70)), textCoord, 0);
                BigBulletsImg.SetActive(true);
                order += 1;
            }
            
            mushroomText.text = (BigBullets.ToString());
        }
        if (Spellbooks > 0)
        {
            if (SpellbookImg.activeSelf == false)
            {
                SpellbookImg.GetComponent<RectTransform>().anchoredPosition = new Vector3((70 + (order*70)), imgCoord, 0);
                spellbookText.GetComponent<RectTransform>().anchoredPosition = new Vector3((70 + (order*70)), textCoord, 0);
                SpellbookImg.SetActive(true);
                order += 1;
            }
            
            spellbookText.text = (Spellbooks.ToString());
        }
        if (FlyingShoes > 0)
        {
            if (FlyingShoesImg.activeSelf == false)
            {
                FlyingShoesImg.GetComponent<RectTransform>().anchoredPosition = new Vector3((70 + (order*70)), imgCoord, 0);
                shoesText.GetComponent<RectTransform>().anchoredPosition = new Vector3((70 + (order*70)), textCoord, 0);
                FlyingShoesImg.SetActive(true);
                order += 1;
            }
            
            shoesText.text = (FlyingShoes.ToString());
        }
        if (Sacrafices > 0) 
        {
            if (SacraficeImg.activeSelf == false)
            {
                SacraficeImg.GetComponent<RectTransform>().anchoredPosition = new Vector3((70 + (order*70)), imgCoord, 0);
                sacraficeText.GetComponent<RectTransform>().anchoredPosition = new Vector3((70 + (order*70)), textCoord, 0);
                SacraficeImg.SetActive(true);
                order += 1;
            }
            
            sacraficeText.text = (Sacrafices.ToString());
        }
        if (FrostSpells > 0)
        {
            if (FrostSpellImg.activeSelf == false)
            {
                FrostSpellImg.GetComponent<RectTransform>().anchoredPosition = new Vector3((70 + (order*70)), imgCoord, 0);
                FrostSpellText.GetComponent<RectTransform>().anchoredPosition = new Vector3((70 + (order*70)), textCoord, 0);
                FrostSpellImg.SetActive(true);
                order += 1;
            }
            
            FrostSpellText.text = (FrostSpells.ToString());
        }
        if (Forcefields > 0)
        {
            if (ForceFieldImg.activeSelf == false)
            {
                ForceFieldImg.GetComponent<RectTransform>().anchoredPosition = new Vector3((70 + (order*70)), imgCoord, 0);
                ForceFieldText.GetComponent<RectTransform>().anchoredPosition = new Vector3((70 + (order*70)), textCoord, 0);
                ForceFieldImg.SetActive(true);
                order += 1;
            }
            
            ForceFieldText.text = (Forcefields.ToString());
        }
        if (Acid > 0)
        {
            if (AcidImg.activeSelf == false)
            {
                AcidImg.GetComponent<RectTransform>().anchoredPosition = new Vector3((70 + (order*70)), imgCoord, 0);
                AcidText.GetComponent<RectTransform>().anchoredPosition = new Vector3((70 + (order*70)), textCoord, 0);
                AcidImg.SetActive(true);
                order += 1;
            }
            
            AcidText.text = (Acid.ToString());
        }
        if (MagicRings > 0)
        {
            if (MagicRingImg.activeSelf == false)
            {
                MagicRingImg.GetComponent<RectTransform>().anchoredPosition = new Vector3((70 + (order*70)), imgCoord, 0);
                MagicRingText.GetComponent<RectTransform>().anchoredPosition = new Vector3((70 + (order*70)), textCoord, 0);
                MagicRingImg.SetActive(true);
                order += 1;
            }
            MagicRingText.text = (MagicRings.ToString());
        }
        if (Bombs > 0)
        {
            if (BombImg.activeSelf == false)
            {
                BombImg.GetComponent<RectTransform>().anchoredPosition = new Vector3((70 + (order*70)), imgCoord, 0);
                BombText.GetComponent<RectTransform>().anchoredPosition = new Vector3((70 + (order*70)), textCoord, 0);
                BombImg.SetActive(true);
                order += 1;
            }
            
            BombText.text = (Bombs.ToString());
        }
        if (FocusCrystals > 0)
        {
          if (FocusCrystalImg.activeSelf == false)
            {
                FocusCrystalImg.GetComponent<RectTransform>().anchoredPosition = new Vector3((70 + (order*70)), imgCoord, 0);
                FocusCrystalText.GetComponent<RectTransform>().anchoredPosition = new Vector3((70 + (order*70)), textCoord, 0);
                FocusCrystalImg.SetActive(true);
                order += 1;
            }
            
            FocusCrystalText.text = (FocusCrystals.ToString());  
        }
        if (Medkits > 0)
        {
          if (MedkitImg.activeSelf == false)
            {
                MedkitImg.GetComponent<RectTransform>().anchoredPosition = new Vector3((70 + (order*70)), imgCoord, 0);
                MedkitText.GetComponent<RectTransform>().anchoredPosition = new Vector3((70 + (order*70)), textCoord, 0);
                MedkitImg.SetActive(true);
                order += 1;
            }
            
            MedkitText.text = (Medkits.ToString());  
        }
        if (EnergyDrinks > 0)
        {
          if (EnergyDrinkImg.activeSelf == false)
            {
                EnergyDrinkImg.GetComponent<RectTransform>().anchoredPosition = new Vector3((70 + (order*70)), imgCoord, 0);
                EnergyDrinkText.GetComponent<RectTransform>().anchoredPosition = new Vector3((70 + (order*70)), textCoord, 0);
                EnergyDrinkImg.SetActive(true);
                order += 1;
            }
            
            EnergyDrinkText.text = (EnergyDrinks.ToString());  
        }
        if (MagicTrees > 0)
        {
          if (MagicTreeImg.activeSelf == false)
            {
                MagicTreeImg.GetComponent<RectTransform>().anchoredPosition = new Vector3((70 + (order*70)), imgCoord, 0);
                MagicTreeText.GetComponent<RectTransform>().anchoredPosition = new Vector3((70 + (order*70)), textCoord, 0);
                MagicTreeImg.SetActive(true);
                order += 1;
            }
            MagicTreeText.text = (MagicTrees.ToString());  
        }
        if (InternalHearts > 0)
        {
          if (InternalHeartImg.activeSelf == false)
            {
                InternalHeartImg.GetComponent<RectTransform>().anchoredPosition = new Vector3((70 + (order*70)), imgCoord, 0);
                InternalHeartText.GetComponent<RectTransform>().anchoredPosition = new Vector3((70 + (order*70)), textCoord, 0);
                InternalHeartImg.SetActive(true);
                order += 1;
            }
            InternalHeartText.text = (InternalHearts.ToString());  
        }
        if (Cauldrons > 0)
        {
          if (CauldronImg.activeSelf == false)
            {
                CauldronImg.GetComponent<RectTransform>().anchoredPosition = new Vector3((70 + (order*70)), imgCoord, 0);
                CauldronText.GetComponent<RectTransform>().anchoredPosition = new Vector3((70 + (order*70)), textCoord, 0);
                CauldronImg.SetActive(true);
                order += 1;
            }
            CauldronText.text = (Cauldrons.ToString());  
        }
        if (StunGuns > 0)
        {
          if (StunGunImg.activeSelf == false)
            {
                StunGunImg.GetComponent<RectTransform>().anchoredPosition = new Vector3((70 + (order*70)), imgCoord, 0);
                StunGunText.GetComponent<RectTransform>().anchoredPosition = new Vector3((70 + (order*70)), textCoord, 0);
                StunGunImg.SetActive(true);
                order += 1;
            }
            StunGunText.text = (StunGuns.ToString());  
        }
        if (Mentors > 0)
        {
          if (MentorImg.activeSelf == false)
            {
                MentorImg.GetComponent<RectTransform>().anchoredPosition = new Vector3((70 + (order*70)), imgCoord, 0);
                MentorText.GetComponent<RectTransform>().anchoredPosition = new Vector3((70 + (order*70)), textCoord, 0);
                MentorImg.SetActive(true);
                order += 1;
            }
            MentorText.text = (Mentors.ToString());  
        }
        if (Angels > 0)
        {
          if (AngelImg.activeSelf == false)
            {
                AngelImg.GetComponent<RectTransform>().anchoredPosition = new Vector3((70 + (order*70)), imgCoord, 0);
                AngelText.GetComponent<RectTransform>().anchoredPosition = new Vector3((70 + (order*70)), textCoord, 0);
                AngelImg.SetActive(true);
                order += 1;
            }
            AngelText.text = (Angels.ToString());  
        }
        if (Magnets > 0)
        {
          if (MagnetImg.activeSelf == false)
            {
                MagnetImg.GetComponent<RectTransform>().anchoredPosition = new Vector3((70 + (order*70)), imgCoord, 0);
                MagnetText.GetComponent<RectTransform>().anchoredPosition = new Vector3((70 + (order*70)), textCoord, 0);
                MagnetImg.SetActive(true);
                order += 1;
            }
            MagnetText.text = (Magnets.ToString());  
        }
        if (Shields > 0)
        {
          if (ShieldImg.activeSelf == false)
            {
                ShieldImg.GetComponent<RectTransform>().anchoredPosition = new Vector3((70 + (order*70)), imgCoord, 0);
                ShieldText.GetComponent<RectTransform>().anchoredPosition = new Vector3((70 + (order*70)), textCoord, 0);
                ShieldImg.SetActive(true);
                order += 1;
            }
            ShieldText.text = (Shields.ToString());  
        }
        if (Vortexes > 0)
        {
          if (VortexImg.activeSelf == false)
            {
                VortexImg.GetComponent<RectTransform>().anchoredPosition = new Vector3((70 + (order*70)), imgCoord, 0);
                VortexText.GetComponent<RectTransform>().anchoredPosition = new Vector3((70 + (order*70)), textCoord, 0);
                VortexImg.SetActive(true);
                order += 1;
            }
            VortexText.text = (Vortexes.ToString());  
        }
        if (Stopwatches > 0)
        {
          if (StopwatchImg.activeSelf == false)
            {
                StopwatchImg.GetComponent<RectTransform>().anchoredPosition = new Vector3((70 + (order*70)), imgCoord, 0);
                StopwatchText.GetComponent<RectTransform>().anchoredPosition = new Vector3((70 + (order*70)), textCoord, 0);
                StopwatchImg.SetActive(true);
                order += 1;
            }
            StopwatchText.text = (Stopwatches.ToString());  
        }
        if (Staffs > 0)
        {
          if (StaffImg.activeSelf == false)
            {
                StaffImg.GetComponent<RectTransform>().anchoredPosition = new Vector3((70 + (order*70)), imgCoord, 0);
                StaffText.GetComponent<RectTransform>().anchoredPosition = new Vector3((70 + (order*70)), textCoord, 0);
                StaffImg.SetActive(true);
                order += 1;
            }
            StaffText.text = (Staffs.ToString());  
        }
        if (Wreaths > 0)
        {
          if (WreathImg.activeSelf == false)
            {
                WreathImg.GetComponent<RectTransform>().anchoredPosition = new Vector3((70 + (order*70)), imgCoord, 0);
                WreathText.GetComponent<RectTransform>().anchoredPosition = new Vector3((70 + (order*70)), textCoord, 0);
                WreathImg.SetActive(true);
                order += 1;
            }
            WreathText.text = (Wreaths.ToString());  
        }
        if (Springers > 0)
        {
          if (SpringImg.activeSelf == false)
            {
                SpringImg.GetComponent<RectTransform>().anchoredPosition = new Vector3((70 + (order*70)), imgCoord, 0);
                SpringText.GetComponent<RectTransform>().anchoredPosition = new Vector3((70 + (order*70)), textCoord, 0);
                SpringImg.SetActive(true);
                order += 1;
            }
            SpringText.text = (Springers.ToString());  
        }
        if (RedHats > 0)
        {
          if (RedHatImg.activeSelf == false)
            {
                RedHatImg.GetComponent<RectTransform>().anchoredPosition = new Vector3((70 + (order*70)), imgCoord, 0);
                RedHatText.GetComponent<RectTransform>().anchoredPosition = new Vector3((70 + (order*70)), textCoord, 0);
                RedHatImg.SetActive(true);
                order += 1;
            }
            RedHatText.text = (RedHats.ToString());  
        }
        if (Wisps > 0)
        {
          if (WispImg.activeSelf == false)
            {
                WispImg.GetComponent<RectTransform>().anchoredPosition = new Vector3((70 + (order*70)), imgCoord, 0);
                WispText.GetComponent<RectTransform>().anchoredPosition = new Vector3((70 + (order*70)), textCoord, 0);
                WispImg.SetActive(true);
                order += 1;
            }
            WispText.text = (Wisps.ToString());  
        }
        if (MusicPlayers > 0)
        {
          if (MusicPlayerImg.activeSelf == false)
            {
                MusicPlayerImg.GetComponent<RectTransform>().anchoredPosition = new Vector3((70 + (order*70)), imgCoord, 0);
                MusicPlayerText.GetComponent<RectTransform>().anchoredPosition = new Vector3((70 + (order*70)), textCoord, 0);
                MusicPlayerImg.SetActive(true);
                order += 1;
            }
            MusicPlayerText.text = (MusicPlayers.ToString());  
        }
        if (LightningHammers > 0)
        {
          if (LightningHammerImg.activeSelf == false)
            {
                LightningHammerImg.GetComponent<RectTransform>().anchoredPosition = new Vector3((70 + (order*70)), imgCoord, 0);
                LightningHammerText.GetComponent<RectTransform>().anchoredPosition = new Vector3((70 + (order*70)), textCoord, 0);
                LightningHammerImg.SetActive(true);
                order += 1;
            }
            LightningHammerText.text = (LightningHammers.ToString());  
        }
        if (HotPeppers > 0)
        {
          if (HotPepperImg.activeSelf == false)
            {
                HotPepperImg.GetComponent<RectTransform>().anchoredPosition = new Vector3((70 + (order*70)), imgCoord, 0);
                HotPepperText.GetComponent<RectTransform>().anchoredPosition = new Vector3((70 + (order*70)), textCoord, 0);
                HotPepperImg.SetActive(true);
                order += 1;
            }
            HotPepperText.text = (HotPeppers.ToString());  
        }
        if (KnowledgeCrystals > 0)
        {
          if (KnowledgeCrystalImg.activeSelf == false)
            {
                KnowledgeCrystalImg.GetComponent<RectTransform>().anchoredPosition = new Vector3((70 + (order*70)), imgCoord, 0);
                KnowledgeCrystalText.GetComponent<RectTransform>().anchoredPosition = new Vector3((70 + (order*70)), textCoord, 0);
                KnowledgeCrystalImg.SetActive(true);
                order += 1;
            }
            KnowledgeCrystalText.text = (KnowledgeCrystals.ToString());  
        }
    }

    // WHEN YOU CLICK ON THE ITEMS

    private Coroutine fadingCoroutine;
    private Coroutine fadingIn;

    public void ShowItemDescription(string description)
    {
        // Stop the current coroutine if it's running
        if (fadingCoroutine != null)
        {
            StopCoroutine(fadingCoroutine);
        }
        if (fadingIn != null)
        {
            StopCoroutine(fadingIn);
        }

        // Start a new coroutine
        fadingCoroutine = StartCoroutine(FadeText(description));
    }

    IEnumerator FadeText(string description)
    {

        // Set the text and reset the alpha to 0
        ItemInfoText.text = description;
        ItemInfoText.color = new Color(ItemInfoText.color.r, ItemInfoText.color.g, ItemInfoText.color.b, 0f);

        // Fade in
        fadingIn = StartCoroutine(FadeTextToFullAlpha(1f, ItemInfoText));

        // yield return StartCoroutine(FadeTextToFullAlpha(1f, ItemInfoText));

        // Wait for 4 seconds
        yield return new WaitForSeconds(4f);

        // Fade out
        yield return StartCoroutine(FadeTextToZeroAlpha(1f, ItemInfoText));
    }

    IEnumerator FadeTextToFullAlpha(float duration, TMP_Text text)
    {
        float time = 0f;

        while (time < duration)
        {
            float alpha = Mathf.Lerp(0f, 1f, time / duration);
            text.color = new Color(text.color.r, text.color.g, text.color.b, alpha);
            time += Time.deltaTime;
            yield return null;
        }

        text.color = new Color(text.color.r, text.color.g, text.color.b, 1f);
    }

    IEnumerator FadeTextToZeroAlpha(float duration, TMP_Text text)
    {
        float time = 0f;

        while (time < duration)
        {
            float alpha = Mathf.Lerp(1f, 0f, time / duration);
            text.color = new Color(text.color.r, text.color.g, text.color.b, alpha);
            time += Time.deltaTime;
            yield return null;
        }

        text.color = new Color(text.color.r, text.color.g, text.color.b, 0f);
    }
}
