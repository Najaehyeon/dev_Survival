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

    private List<int> selectedEmployeeIndexes = new List<int>(); // 뽑힌 직원 인덱스 리스트

    private void Awake()
    {
        ShopManager.Instance.employShop = this;
    }

    private void Start()
    {
        rerollButon.onClick.AddListener(Reroll);
        Reroll();
    }

    void HireFirstEmployee()
    {
        GameManager.Instance.ChangeMoney(-allEmployees[selectedEmployeeIndexes[0]].Price); // 금액 지불
        // 구매 안 한 리스트에서 빼야함.
        // 직원 구매했다고 전달 해야함.
        EmployeeManager.Instance.HireEmployee(selectedEmployeeIndexes[0]);
    }

    void HireSecondEmployee()
    {
        GameManager.Instance.ChangeMoney(-allEmployees[selectedEmployeeIndexes[1]].Price); // 금액 지불
        EmployeeManager.Instance.HireEmployee(selectedEmployeeIndexes[1]);
    }

    void HireThirdEmployee()
    {
        GameManager.Instance.ChangeMoney(-allEmployees[selectedEmployeeIndexes[2]].Price); // 금액 지불
        EmployeeManager.Instance.HireEmployee(selectedEmployeeIndexes[2]);
    }

    public void Reroll()
    {
        List<int> newEmployees = new List<int>();

        while (newEmployees.Count < 3)
        {
            int employeeNum = Random.Range(0, allEmployees.Length); // 직원 수에 맞게 조정

            if (!selectedEmployeeIndexes.Contains(employeeNum) && !newEmployees.Contains(employeeNum))
            {
                newEmployees.Add(employeeNum);
            }
        }

        selectedEmployeeIndexes = newEmployees;

        UpdateUI();

        firstEmployButton.onClick.AddListener(HireFirstEmployee);
        secondEmployButton.onClick.AddListener(HireSecondEmployee);
        thirdEmployButton.onClick.AddListener(HireThirdEmployee);
    }

    void UpdateUI()
    {
        if (selectedEmployeeIndexes.Count < 3) return;

        // 뽑힌 직원 데이터를 가져옴
        EmployData first = allEmployees[selectedEmployeeIndexes[0]];
        EmployData second = allEmployees[selectedEmployeeIndexes[1]];
        EmployData third = allEmployees[selectedEmployeeIndexes[2]];

        // UI 업데이트
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

        firstEmployeeStressControl.text = first.StressControl.ToString("F1");
        secondEmployeeStressControl.text = second.StressControl.ToString("F1");
        thirdEmployeeStressControl.text = third.StressControl.ToString("F1");
    }
}
