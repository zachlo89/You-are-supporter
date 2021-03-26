using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateFaceAndBody : MonoBehaviour
{
    [SerializeField] private SpriteRenderer headSprites;
    [SerializeField] private SpriteRenderer hairsSprites;
    [SerializeField] private SpriteRenderer eyesSprites;
    [SerializeField] private SpriteRenderer eyebrowsSprites;
    [SerializeField] private SpriteRenderer earsSprites;
    [SerializeField] private SpriteRenderer mouthSprites;
    [SerializeField] private SpriteRenderer beardSprites;
    [SerializeField] private SpriteRenderer torso;
    [SerializeField] private SpriteRenderer pelvis;
    [SerializeField] private SpriteRenderer armL;
    [SerializeField] private SpriteRenderer forearmoL;
    [SerializeField] private SpriteRenderer armR;
    [SerializeField] private SpriteRenderer forearmR;
    [SerializeField] private SpriteRenderer handL;
    [SerializeField] private SpriteRenderer handR;
    [SerializeField] private SpriteRenderer legL;
    [SerializeField] private SpriteRenderer legR;
    [SerializeField] private SpriteRenderer shinL;
    [SerializeField] private SpriteRenderer shinR;
    [SerializeField] private Color bodyColor;
    [SerializeField] private Color hairColor;

    private ScriptableCharacter character;

    private void Start()
    {
        if(character != null)
        {
            SetUpFace(character);
        }
    }

    public void SetCharacter(ScriptableCharacter character)
    {
        this.character = character;
    }

    public void SetUpFace(ScriptableCharacter character)
    {
        this.character = character;
        headSprites.sprite = character.headSprites;
        hairsSprites.sprite = character.hairsSprites;
        eyebrowsSprites.sprite = character.eyebrowsSprites;
        eyesSprites.sprite = character.eyesSprites;
        earsSprites.sprite = character.earsSprites;
        mouthSprites.sprite = character.mouthSprites;
        beardSprites.sprite = character.beardSprites;
        torso.sprite = character.torso;
        pelvis.sprite = character.pelvis;
        armL.sprite = character.armL;
        forearmoL.sprite = character.forearmoL;
        armR.sprite = character.armR;
        forearmR.sprite = character.forearmR;
        handL.sprite = character.handL;
        handR.sprite = character.handR;
        legL.sprite = character.legL;
        legR.sprite = character.legR;
        shinL.sprite = character.shinL;
        shinR.sprite = character.shinR;

        bodyColor = character.bodyColor;
        hairColor = character.hairColor;
        headSprites.color = bodyColor;
        torso.color = bodyColor;
        pelvis.color = bodyColor;
        armL.color = bodyColor;
        forearmoL.color = bodyColor;
        armR.color = bodyColor;
        forearmR.color = bodyColor;
        handL.color = bodyColor;
        handR.color = bodyColor;
        legL.color = bodyColor;
        legR.color = bodyColor;
        shinL.color = bodyColor;
        shinR.color = bodyColor;
        earsSprites.color = bodyColor;

        hairsSprites.color = hairColor;
        beardSprites.color = hairColor;
    }
}
