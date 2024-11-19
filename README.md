## 구조
GameManager로 부터 시작되어 스레드가 제어되도록 만듭니다.

각 Phase에 따라 필요한 기능을 제어합니다.
ex) PhaseInitialize, PhaseStage

각 기능별로 모듈화하여 확장성을 부여하는 방식으로 개발합니다.
ex) PlayerController 하위 System들.
