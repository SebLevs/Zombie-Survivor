using UnityEngine;

public class BossHealthBarSubscriber : MonoBehaviour
{
    // TODO: NOTE: May need refactoring onenable and ondisable if multiple bosses are to be active at once ...
    // ... OnEnable would InitBossHealthBar differently: Pull from  a pool of Filling bar_Boss
    // ... OnDisable would not hide the viewBossHealthBars, but return a Filling bar_Boss to a pool of filling bars
    // ... Pool would be set inside of ViewBossHealthBars.cs

    [SerializeField] private EnemyType type;
    private ViewFillingBarWithTextElement m_viewBossHealthBar;

    private void OnEnable()
    {
        UIManager uiManager = UIManager.Instance;
        uiManager.ViewBossHealthBars.OnShow();
        m_viewBossHealthBar = uiManager.ViewBossHealthBars.InitBossHealthBar(uiManager.ViewBossHealthBars.ViewBossHealthBar, type);
    }

    private void OnDisable()
    {
        if (!UIManager.Instance) { return; }
        UIManager uiManager = UIManager.Instance;
        uiManager.ViewBossHealthBars.OnHide();
        m_viewBossHealthBar = null;
    }

    public void SetHealthBar(Health health)
    {
        m_viewBossHealthBar.Filler.SetFilling(health);
    }
}
