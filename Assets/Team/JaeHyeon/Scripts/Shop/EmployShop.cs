using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EmployShop : MonoBehaviour
{
    public Button rerollButon;
    public Button firstEmployButton;
    public Button secondEmployButton;
    public Button thirdEmployButton;

    public TextMeshProUGUI moneyInEmployShop;

    [Header("직원 데이터")]
    [SerializeField] private EmployData[] allEmployees; // 모든 직원 데이터 저장
    private HashSet<int> hiredEmployees = new HashSet<int>(); // 고용된 직원 추적

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

    private List<int> selectedEmployeeIndexes = new List<int>(); // 뽑힌 직원 인덱스 리스트

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
        if (GameManager.Instance.Money < allEmployees[employeeID].Price) return;
        if (hiredEmployees.Contains(employeeID)) return; // 이미 고용된 직원이면 실행 안 함

        // 금액 차감 및 UI 갱신
        GameManager.Instance.ChangeMoney(-allEmployees[employeeID].Price);
        MoneyInit();

        // 직원 고용
        EmployeeManager.Instance.HireEmployee(employeeID);

        // 고용된 직원 목록에 추가
        hiredEmployees.Add(employeeID);

        // 리롤을 강제 실행하여 고용된 직원 제거
        Reroll();
    }

    public void Reroll()
    {
        List<int> newEmployees = new List<int>();

        while (newEmployees.Count < (hiredEmployees.Count < 6 ? 3 : 8 - hiredEmployees.Count))
        {
            int employeeNum = Random.Range(0, allEmployees.Length);

            // 이미 고용된 직원이 아니고, 중복이 아닌 경우에만 추가
            if (!hiredEmployees.Contains(employeeNum) && !newEmployees.Contains(employeeNum))
            {
                newEmployees.Add(employeeNum);
            }
        }

        selectedEmployeeIndexes = newEmployees;
        UpdateUI();

        // 기존 리스너 제거 후 새로 추가 (중복 실행 방지)
        firstEmployButton.onClick.RemoveAllListeners();
        secondEmployButton.onClick.RemoveAllListeners();
        thirdEmployButton.onClick.RemoveAllListeners();

        firstEmployButton.onClick.AddListener(() => HireEmployee(0));
        secondEmployButton.onClick.AddListener(() => HireEmployee(1));
        thirdEmployButton.onClick.AddListener(() => HireEmployee(2));
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
