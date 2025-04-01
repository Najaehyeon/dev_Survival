using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EmployShop : MonoBehaviour
{
    public Button rerollButon;
    public Button[] employButton;

    public TextMeshProUGUI moneyInEmployShop;

    [Header("직원 데이터")]
    [SerializeField] private EmployData[] allEmployees; // 모든 직원 데이터 저장

    [Header("Name")]
    [SerializeField] private TextMeshProUGUI firstEmployeeNameText;
    [SerializeField] private TextMeshProUGUI secondEmployeeNameText;
    [SerializeField] private TextMeshProUGUI thirdEmployeeNameText;

    [Header("Icon")]
    [SerializeField] private Image firstEmployeeIcon;
    [SerializeField] private Image secondEmployeeIcon;
    [SerializeField] private Image thirdEmployeeIcon;

    [Header("Price")]
    [SerializeField] private TextMeshProUGUI firstEmployeePrice;
    [SerializeField] private TextMeshProUGUI secondEmployeePrice;
    [SerializeField] private TextMeshProUGUI thirdEmployeePrice;

    [Header("Ability")]
    [SerializeField] private TextMeshProUGUI firstEmployeeAbility;
    [SerializeField] private TextMeshProUGUI secondEmployeeAbility;
    [SerializeField] private TextMeshProUGUI thirdEmployeeAbility;

    [Header("Sincerity")]
    [SerializeField] private TextMeshProUGUI firstEmployeeSincerity;
    [SerializeField] private TextMeshProUGUI secondEmployeeSincerity;
    [SerializeField] private TextMeshProUGUI thirdEmployeeSincerity;

    [Header("StressControl")]
    [SerializeField] private TextMeshProUGUI firstEmployeeStressControl;
    [SerializeField] private TextMeshProUGUI secondEmployeeStressControl;
    [SerializeField] private TextMeshProUGUI thirdEmployeeStressControl;

    private List<int> selectedEmployeeIndexes = new List<int>(); // 선택된 직원 인덱스
    private List<int> hiredEmployeeIDs = new List<int>(); // 고용된 직원 ID 저장

    private void Awake()
    {
        ShopManager.Instance.employShop = this;
    }

    private void Start()
    {
        rerollButon.onClick.AddListener(Reroll);
        Reroll();
        MoneyInit();
    }

    void HireEmployee(int index)
    {
        if (index < 0 || index >= selectedEmployeeIndexes.Count) return;

        int employeeID = selectedEmployeeIndexes[index];
        if (GameManager.Instance.Money < allEmployees[employeeID].Price)
        {
            UIManager.Instance.shopUI.notEnoughAlert.SetActive(true);

            UIManager.Instance.shopUI.closeNotEnoughMoneyAlert.onClick.AddListener(UIManager.Instance.shopUI.CloseNotEnoughMoneyAlert);
            return;
        }

        // 금액 차감 및 UI 갱신
        GameManager.Instance.ChangeMoney(-allEmployees[employeeID].Price);
        MoneyInit();

        // 직원 고용
        EmployeeManager.Instance.HireEmployee(employeeID);

        // 고용된 직원 목록에 추가
        hiredEmployeeIDs.Add(employeeID);

        // 버튼 비활성화
        employButton[index].gameObject.SetActive(false);
    }

    public void Reroll()
    {
        if (hiredEmployeeIDs.Count == 8) return;

        List<int> newEmployees = new List<int>();

        while (newEmployees.Count < 3)
        {
            int employeeNum = Random.Range(0, allEmployees.Length);

            // 중복이 아닌 경우에만 추가
            if (!newEmployees.Contains(employeeNum))
            {
                newEmployees.Add(employeeNum);
            }
        }

        selectedEmployeeIndexes = newEmployees;
        UpdateUI();

        // 버튼 설정
        for (int i = 0; i < selectedEmployeeIndexes.Count; i++)
        {
            int index = i;
            employButton[i].onClick.RemoveAllListeners();
            employButton[i].onClick.AddListener(() => HireEmployee(index));

            int employeeID = selectedEmployeeIndexes[i];

            // 이미 고용된 직원이면 버튼 비활성화
            employButton[i].gameObject.SetActive(!hiredEmployeeIDs.Contains(employeeID));
        }
    }

    void UpdateUI()
    {
        if (selectedEmployeeIndexes.Count < 3) return;

        EmployData first = allEmployees[selectedEmployeeIndexes[0]];
        EmployData second = allEmployees[selectedEmployeeIndexes[1]];
        EmployData third = allEmployees[selectedEmployeeIndexes[2]];

        firstEmployeeNameText.text = first.EmployName;
        secondEmployeeNameText.text = second.EmployName;
        thirdEmployeeNameText.text = third.EmployName;

        firstEmployeeIcon.sprite = first.EmployIcon;
        secondEmployeeIcon.sprite = second.EmployIcon;
        thirdEmployeeIcon.sprite = third.EmployIcon;

        firstEmployeePrice.text = first.Price.ToString();
        secondEmployeePrice.text = second.Price.ToString();
        thirdEmployeePrice.text = third.Price.ToString();

        firstEmployeeAbility.text = first.Ability.ToString();
        secondEmployeeAbility.text = second.Ability.ToString();
        thirdEmployeeAbility.text = third.Ability.ToString();

        firstEmployeeSincerity.text = first.Sincerity.ToString();
        secondEmployeeSincerity.text = second.Sincerity.ToString();
        thirdEmployeeSincerity.text = third.Sincerity.ToString();

        firstEmployeeStressControl.text = first.StressControl.ToString("F2");
        secondEmployeeStressControl.text = second.StressControl.ToString("F2");
        thirdEmployeeStressControl.text = third.StressControl.ToString("F2");
    }

    public void MoneyInit()
    {
        moneyInEmployShop.text = GameManager.Instance.Money.ToString() + "\\";
    }
}
