using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EmployShop : MonoBehaviour
{
    public GameObject fullEmployeeAlert;

    [Header("Buttons")]
    public Button closeFullEmployeeAlert;
    public Button rerollButon;
    public Button[] employButton;

    [Header("Text")]
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

    private ShopUI shopUI;

    private void Start()
    {
        shopUI = UIManager.Instance.shopUI;
        rerollButon.onClick.AddListener(Reroll);
        Reroll();
        shopUI.MoneyInit();
    }

    void HireEmployee(int index)
    {
        int employeeID = selectedEmployeeIndexes[index];

        if (hiredEmployeeIDs.Count == 3)
        {
            fullEmployeeAlert.SetActive(true);
            closeFullEmployeeAlert.onClick.AddListener(() => fullEmployeeAlert.SetActive(false));
            return;
        }

        // 인덱스 벗어나면
        if (index < 0 || index >= selectedEmployeeIndexes.Count) return;

        // 돈 부족 시
        if (GameManager.Instance.Money < allEmployees[employeeID].Price)
        {
            shopUI.notEnoughMoneyAlert.SetActive(true);
            shopUI.closeNotEnoughMoneyAlert.onClick.AddListener(shopUI.CloseNotEnoughAlert);
            return;
        }

        // 금액 차감 및 UI 갱신
        GameManager.Instance.ChangeMoney(-allEmployees[employeeID].Price);
        shopUI.MoneyInit();

        // 직원 고용
        EmployeeManager.Instance.HireEmployee(employeeID);

        // 고용된 직원 목록에 추가
        hiredEmployeeIDs.Add(employeeID);

        // 버튼 비활성화
        employButton[index].gameObject.SetActive(false);
    }

    public void Reroll()
    {
        // 모두 채용하면 리롤 안 되게
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

        UpdateUI();
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
}
