using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class VisualEffectManager : MonoBehaviour
{
    public List<SkillVisualEffect> skillVisualEffects;
    public List<CardVisualEffect> cardVisualEffects = new List<CardVisualEffect>();

    private SkillVisualEffect.Factory _skillVisualEffectFactory;
    private CardVisualEffect.Factory _cardVisualEffectFactory;
    private FloatyNumber.Factory _floatyNumberFactory;

    private DiContainer _container;

    [Inject]
    public void Construct(SkillVisualEffect.Factory skillVisualEffectFactory,
                          CardVisualEffect.Factory cardVisualEffectFactory,
                          FloatyNumber.Factory floatyNumberFactory,
                          DiContainer container)
    {
        _skillVisualEffectFactory = skillVisualEffectFactory;
        _cardVisualEffectFactory = cardVisualEffectFactory;
        _floatyNumberFactory = floatyNumberFactory;
        _container = container;
    }

    public void Update()
    {
        skillVisualEffects.RemoveAll(effect => effect == null || !effect.gameObject.activeSelf);
        foreach (var visualEffect in skillVisualEffects)
        {
            visualEffect.Update();
        }
        skillVisualEffects.RemoveAll(effect => effect == null || !effect.gameObject.activeSelf);

        cardVisualEffects.RemoveAll(effect => effect == null || !effect.gameObject.activeSelf);
        foreach (var visualEffect in cardVisualEffects)
        {
            visualEffect.Update();
        }
        cardVisualEffects.RemoveAll(effect => effect == null || !effect.gameObject.activeSelf);
    }

    //TODO fix this fireball animation
    public CardVisualEffect CreateCardVisualEffect(string effectName, Tile target)
    {
        CardVisualEffect cardEffect = _cardVisualEffectFactory.Create();
        
        if (effectName == "Fireball")
        {
            if (target.monster.team == "Enemy")
            {
                cardEffect.Initialize(new Vector2(-1700, Screen.height / 2 + 600), target.transform.position, effectName);
                cardEffect.transform.rotation = Quaternion.Euler(0, 0, 90f);
                cardVisualEffects.Add(cardEffect);
                cardEffect.reachedTarget.AddListener(() => {
                    cardEffect.reachedTarget.RemoveAllListeners();
                });
                return cardEffect;
            } else
            {
                cardEffect.Initialize(new Vector2(1700, Screen.height / 2 + 600), target.transform.position, effectName);
                cardEffect.transform.rotation = Quaternion.Euler(0, 180f, 90f);
                cardVisualEffects.Add(cardEffect);
                cardEffect.reachedTarget.AddListener(() => {
                    cardEffect.reachedTarget.RemoveAllListeners();
                });
                return cardEffect;
            }
        }
        return null;
    }

    public void CreateSkillVisualEffect(string effectName, Tile target, Vector2 start, int damage)
    {
        SkillVisualEffect skillEffect = _skillVisualEffectFactory.Create();
        skillEffect.Initialize(start, target.transform.position, effectName);
        skillVisualEffects.Add(skillEffect);

        skillEffect.reachedTarget.AddListener(() => {
            skillEffect.reachedTarget.RemoveAllListeners();
            target.monster.Health -= damage;
            CreateFloatyNumber(damage, target, true);
        });
    }

    public void CreateFloatyNumber(int number, Tile target, bool isDamage)
    {
        FloatyNumber floatyNumber = _floatyNumberFactory.Create();
        floatyNumber.Initialize(number, target.transform.position, isDamage);
    }

    public void CreateFloatyNumber(int number, Vector3 position, bool isDamage)
    {
        FloatyNumber floatyNumber = _floatyNumberFactory.Create();
        floatyNumber.Initialize(number, position, isDamage);
    }
}