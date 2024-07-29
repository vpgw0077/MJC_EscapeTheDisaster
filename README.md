# Escape Disaster
![캡처](https://github.com/user-attachments/assets/0198f50c-d308-4e7b-bb91-9492f9f9902f)

## 개요
- 프로젝트 이름 : Escape Disaster
- 장르 : 퍼즐, 3인칭, 싱글플레이
- 개발 도구 : Unity ver.2018.4.22f1, C#
- 개발 인원 : 3명
- 담당 역할 : 게임 기획, 플레이어, 퍼즐 등 핵심 기능 개발, 레벨 디자인
- [플레이 영상](https://youtu.be/E323XEek5Og)

## 구동 방법
프로젝트를 열고 Assets -> Scenes -> New Scene을 열고 플레이하면 됩니다.

## 프로젝트 설명
- 플레이어는 바이러스로 인해 황폐화 된 도시를 정화하는 로봇이 되어 난관을 헤쳐나가야 합니다!
- Escape Disaster는 스테이지에 있는 주변 오브젝트와 장치들을 활용해 바이러스를 모두 제거해 스테이지를 클리어하는 퍼즐 게임입니다.
- 총 10개의 스테이지로 구성되어 있으며 멀티 플레잉은 지원하지 않습니다.

## 인 게임 요소
#### 플레이어 조작

|이동|점프|상호작용, 내려놓기| 던지기|
| :---: |:---:|:---:|:---:|
|WASD| Space |E|Left Mouse|

#### 스테이지 클리어<br>
플레이어는 퍼즐을 풀어 바이러스에 도달해 파괴한 후 DNA 오브젝트를 얻어야 한다.<br>
그 후 DNA를 특정 지역에 두고 문을 열어야 한다.

![213-ezgif com-video-to-gif-converter](https://github.com/user-attachments/assets/5d997f26-65c3-47b3-8525-87baabb479f6)

|일반 바이러스| 슈퍼 바이러스 |바이러스 DNA|백신|
| :---: |:---:|:---:|:---:|
|![cv](https://github.com/user-attachments/assets/41f9061e-ad4a-4b33-8092-8cab1815aa8a)|![sv](https://github.com/user-attachments/assets/41dc53ad-a45a-4916-85b1-827b7aa0926d)|![vd](https://github.com/user-attachments/assets/88a5cf9b-e7ab-4816-8111-5b80889ca9a2)|![v](https://github.com/user-attachments/assets/512b1c0a-d501-47c8-8c11-66c7ea068f71)|
|백신, 플레이어와<br>닿으면 사라진다.| 백신 오브젝트와<br>닿으면 사라진다. |문을 열 수 있는 열쇠|바이러스를 없앨 수 있는<br>오브젝트|

#### 퍼즐 요소 몇 가지

- 불을 이용한 퍼즐 : 특정 오브젝트에 불을 붙여 스테이지를 공략 해야한다.

![_2024_07_29_20_46_35_325-ezgif com-video-to-gif-converter](https://github.com/user-attachments/assets/fce5a89f-95af-47d1-8675-2a4a165caf4b)

- 함정 발판 : 센서에 무언가가 감지되면 함정 벽이 발동한다.
  
![_2024_07_29_20_50_16_423-ezgif com-video-to-gif-converter](https://github.com/user-attachments/assets/2497346e-f12e-494b-a8fd-af95b78f3f41)

- 전선 파이프 : 문을 열거나 동력을 연결하는 등의 역할을 하는 오브젝트
  
![_2024_07_29_20_54_47_43-ezgif com-video-to-gif-converter](https://github.com/user-attachments/assets/a5ea4cbe-532a-45cb-af45-719e66c86fe9)

- 파란 푸쉬 파이프 : 오브젝트를 밀어내는 바람을 발사하는 장치
  
![_2024_07_29_21_15_47_92-ezgif com-video-to-gif-converter](https://github.com/user-attachments/assets/2ac133fd-0842-411a-ae0f-9894bcbf1477)

- 바람 하수구 : 오브젝트를 위로 띄워주는 장치로 다른 하수구를 막으면 나머지 하수구의 풍력이 세지는 매커니즘을 가지고 있음.
  
|![_2024_07_29_21_46_49_671-ezgif com-video-to-gif-converter](https://github.com/user-attachments/assets/c81aa39b-0178-48a1-935a-bfea70222831)|![_2024_07_29_21_47_12_959-ezgif com-video-to-gif-converter](https://github.com/user-attachments/assets/4ec8375f-a3d7-4961-9318-732e0c6e1525)| ![_2024_07_29_21_47_33_587-ezgif com-video-to-gif-converter](https://github.com/user-attachments/assets/f447b418-3489-45bf-9195-51611fe374d7) |
| :---: |:---:|:---:|
|0개 막았을 때|1개 막았을 때|3개 막았을 때|
