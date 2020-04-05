using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MapGenerator : MonoBehaviour
{
    int[,] GenerateArray = new int[5, 5];//랜덤으로 만들어지는 배열
    bool[,] TrailCheckArray = new bool[5, 5];//블럭이 지나갔었는지 저장하는 배열, bool형태이다
    bool MoveEnd = false;//이동이 다 끝났는지
    int Direction = 0;
    int CurDirection = 0;
    int CurrentY;
    int CurrentX;
    int TempY;
    int TempX;
    int FinalBlockY;
    int FinalBlockX;
    int CurrentCount = 1;//점점 추가할 숫자(Difficult에 맞출때 까지)
    int MakeRandom = 0;
    bool isPlaceGood = false;//생성 위치가 올바른지

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void CreateMap(int SetDifficult)
    {
       int[,] GenerateArray = new int[5, 5];//랜덤으로 만들어지는 배열
       int[,] WallCheckArray = new int[5, 5];//벽이 어케 이루어 져있는지 저장하는 배열

        //--------------------------------------------------------------------------초기화--------------------------------------------------------
        for (int y = 0; y < 5; y++)
        {
            for (int x = 0; x < 5; x++)
            {
                    GenerateArray[y, x] = 0;//전체 초기화
                    WallCheckArray[y, x] = 0;//전체 초기화
            }
        }



        //--------------------------------------------------------------------------숫자블럭 생성--------------------------------------------------------

        GenerateArray[Random.Range(0, 5), Random.Range(0, 5)] = SetDifficult;//최상위 숫자는 두개가 존재해야하기 때문에 따로 한번 더 생성, ex)4, 4
        for (int Count = SetDifficult; Count > 0; Count--)//랜덤 블럭 생성기
        {
            int randX = Random.Range(0, 5);
            int randY = Random.Range(0, 5);

            if (GenerateArray[randY, randX] != 0)//0이 아니면(다른 값이 채워졌다면)
                Count++;//재기회
            else
                GenerateArray[randY, randX] = Count;//0이면 거기에 값 넣기
        }
        //--------------------------------------------------------------------------X블럭 생성--------------------------------------------------------
        for (int y = 0; y < 5; y++)
        {
            for (int x = 0; x < 5; x++)
            {
                if ((y == 0 || y == 4) && (x == 0 || x == 4))//모서리 담당
                {

                }
            }
        }

        int RandomCount = Random.Range(5, 14);//블록을 생성할 갯수
        Debug.Log("This Stage has XBlock " + RandomCount);
        while (RandomCount > 0)
        {
            int RanX = Random.Range(0, 5);
            int RanY = Random.Range(0, 5);
            //if (GenerateArray[RanY, RanX] == 0 && ((GenerateArray[RanY-1, RanX] != 255 || GenerateArray[RanY + 1, RanX] != 255)
            //    &&(GenerateArray[RanY, RanX - 1] != 255|| GenerateArray[RanY, RanX + 1] != 255)))//아무데나 지정하고 그곳이 비어있다면
                if (GenerateArray[RanY, RanX] == 0 )
                {

                //if(Random.Range(0, 2) == 1)//bool형 랜덤
                    GenerateArray[RanY, RanX] = 255;//x블록 생성

                RandomCount--;//카운트 줄어들음
                }
        }
        //--------------------------------------------------------------------------저장 및 다음 씬 이동--------------------------------------------------------
        EditManager.TestArray = GenerateArray;
        Debug.Log("Generate Success");
        SceneManager.LoadScene("New_EditingScene");
    }






    public void CreateMap2(int SetDifficult)//기존 완벽 무작위 생성의 담당일찐 코드
    {
        //---------------------------------------------------------------------------초기화--------------------------------------------
        for (int y = 0; y < 5; y++)
        {
            for (int x = 0; x < 5; x++)
            {
                GenerateArray[y, x] = 0;//전체 초기화
                TrailCheckArray[y, x] = false;//전체 초기화
            }
        }

        //---------------------------------------------------------------------------X블록 생성--------------------------------------------
        int RandomCount = Random.Range(5, 10);//블록을 생성할 갯수
        //Debug.Log("This Stage has XBlock " + RandomCount);

        while (RandomCount > 0)
        {
            int RanX = Random.Range(0, 5);
            int RanY = Random.Range(0, 5);
                    GenerateArray[RanY, RanX] = 255;//x블록 생성
                    TrailCheckArray[RanY, RanX] = true;//무언가가 있따는걸 확인
                RandomCount--;//카운트 줄어들음
        }

        //---------------------------------------------------------------------------시작 블록 생성--------------------------------------------
        FindStartPlace:

        CurrentY = Random.Range(0, 5);
        CurrentX = Random.Range(0, 5);
        if (TrailCheckArray[CurrentY, CurrentX])//이미 X블록이 존재하면
            goto FindStartPlace;//재탐사
        //Debug.Log("We Start at " + CurrentX + "," +CurrentY);

        

        GenerateArray[CurrentY, CurrentX] = CurrentCount;//시작하는 지점의 블럭은 1
        TempY = CurrentY;//나중에 다음 숫자 블럭을 생성할때 좌표가 같은지 확인용
        TempX = CurrentX;
        CurrentCount++;//이제 2부터
        TrailCheckArray[CurrentY, CurrentX] = true;//무언가가 있따는걸 확인
        MoveEnd = false;//움직일 준비를 해줌

        for (CurrentCount = 2; CurrentCount <= SetDifficult;)
        {

        //isPlaceGood = false;
        //while (!isPlaceGood)
        //{
            PlaceCheck();//오브젝트의 위치가 문제인지 아닌지 확인하는 코드, 매 for문에서 긴 코드를 실행하기 그래서 함수로 만듬.
        //}
            //Direction = Random.Range(1, 5);//위 코드 대치용

                //Debug.Log("MyDirection is " + Direction);
            if (!MoveEnd)
            MoveFunction();//생성하자~
        }

        FindFinalPlace://마지막 남은 블럭을 무작위 위치에 설치
        FinalBlockY = Random.Range(0, 5);
        FinalBlockX = Random.Range(0, 5);

        if (TrailCheckArray[FinalBlockY, FinalBlockX])//이미 무언가가 있다면
            goto FindFinalPlace;

        GenerateArray[FinalBlockY, FinalBlockX] = SetDifficult;//마지막 숫자(합체가 되기 위한) 설치
        //Debug.Log("We Made Final Number " + CurrentCount + " Block at" + CurrentY + "," + CurrentX);

        //--------------------------------------------------------------------------저장 및 다음 씬 이동--------------------------------------------------------
        EditManager.TestArray = GenerateArray;
        //Debug.Log("Generate Success");
        SceneManager.LoadScene("New_EditingScene");
    }

    void MoveFunction()
    {
            switch (Direction)
            {
                case 2://왼쪽

                    if (CurrentY == 0)//더 이상 갈 곳이 없다던지, 다른게 있던지
                    {
                        MoveEnd = true;//단방향 이동 끝!
                    }
                    else if(TrailCheckArray[CurrentY - 1, CurrentX] == true)
                    {
                        MoveEnd = true;//단방향 이동 끝!
                    }
                    else
                    {
                        --CurrentY;//한칸 이동
                        TrailCheckArray[CurrentY, CurrentX] = true;//방명록 남기는 거임.
                    }
                    break;
                case 1://위
                    if (CurrentX == 0)//더 이상 갈 곳이 없다던지, 다른게 있던지
                    {
                        MoveEnd = true;//단방향 이동 끝!
                    }
                    else if (TrailCheckArray[CurrentY, CurrentX - 1] == true)
                    {
                        MoveEnd = true;//단방향 이동 끝!
                    }
                    else
                    {
                        --CurrentX;//한칸 이동
                        TrailCheckArray[CurrentY, CurrentX] = true;//방명록 남기는 거임.
                    }
                    break;
                case 4://오른쪽
                    if (CurrentY == 4)//더 이상 갈 곳이 없다던지, 다른게 있던지
                    {
                    MoveEnd = true;//단방향 이동 끝!
                    }
                else if (TrailCheckArray[CurrentY + 1, CurrentX] == true)
                    {
                        MoveEnd = true;//단방향 이동 끝!
                    }
                else
                    {
                        ++CurrentY;//한칸 이동
                        TrailCheckArray[CurrentY, CurrentX] = true;//방명록 남기는 거임.
                    }
                    break;
                case 3://아래
                if ( CurrentX == 4)//더 이상 갈 곳이 없다던지, 다른게 있던지
                    {
                        MoveEnd = true;//단방향 이동 끝!
                    }
                else if (TrailCheckArray[CurrentY, CurrentX + 1] == true)
                    {
                        MoveEnd = true;//단방향 이동 끝!
                    }
                else
                    {
                        ++CurrentX;//한칸 이동
                        TrailCheckArray[CurrentY, CurrentX] = true;//방명록 남기는 거임.
                    }
                    break;
            }
        if (MoveEnd)
        {
            MakeRandom = Random.Range(0, 2);//생성할지 말지
            if (MakeRandom == 1)
            {
                GenerateArray[CurrentY, CurrentX] = CurrentCount;//지금 값을 저장
                Debug.Log("We Made " + CurrentCount + " Block at" + CurrentY + "," + CurrentX);
                TrailCheckArray[CurrentY, CurrentX] = true;//무언가가 있따는걸 확인
                CurrentCount++;//값 추가
                MoveEnd = false;//다시 움직일 수 있게 MoveEnd를 풀어줌
            }
            else//블럭을 생성안하고 다시 지역 탐사
            {
                MoveEnd = false;//다시 움직일 수 있게 MoveEnd를 풀어줌
            }
        }
        //if (MoveEnd)
        //{
        //        GenerateArray[CurrentY, CurrentX] = CurrentCount;//지금 값을 저장
        //        //Debug.Log("We Made " + CurrentCount + " Block at" + CurrentY + "," + CurrentX);
        //        TrailCheckArray[CurrentY, CurrentX] = true;//무언가가 있따는걸 확인
        //        CurrentCount++;//값 추가
        //        MoveEnd = false;//다시 움직일 수 있게 MoveEnd를 풀어줌
        //}
    }

    int ChoiceRandom(int min, int max, int except)//선택적 랜덤
    {
        int temp = 0;

        do
        {
            temp = Random.Range(min, max);
        } while (temp == except);
            return temp;
    }

    void PlaceCheck()
    {
        //CurDirection = Random.Range(1, 5);//이동 방향 조정하기
        //if ((CurDirection == 2 && (CurrentY == 0 || TrailCheckArray[CurrentY - 1, CurrentX]))//한칸도 못움직이는 상황이 연출된다면
        //    || (CurDirection == 1 && (CurrentX == 0 || TrailCheckArray[CurrentY, CurrentX - 1]))
        //    || (CurDirection == 4 && (CurrentY == 4 || TrailCheckArray[CurrentY + 1, CurrentX]))
        //    || (CurDirection == 3 && (CurrentX == 4 || TrailCheckArray[CurrentY, CurrentX + 1])))

        //if (CurDirection == 2 && (CurrentY == 0 || TrailCheckArray[CurrentY - 1, CurrentX]))//한칸도 못움직이는 상황이 연출된다면
        //{
        //}
        //else if (CurDirection == 1 && (CurrentX == 0 || TrailCheckArray[CurrentY, CurrentX - 1]))
        //{ }
        //else if (CurDirection == 4 && (CurrentY == 4 || TrailCheckArray[CurrentY + 1, CurrentX]))
        //{ }
        //else if(CurDirection == 3 && (CurrentX == 4 || TrailCheckArray[CurrentY, CurrentX + 1]))
        //{ }
        //else if (CurDirection == Direction)//전 방향과 같다면
        //{ }
        RandomStart:
        if (CurrentY == 0 || TrailCheckArray[CurrentY - 1, CurrentX])//한칸도 못움직이는 상황이 연출된다면
        {
            CurDirection = ChoiceRandom(1, 5, 2);
        }
        else if (CurrentX == 0 || TrailCheckArray[CurrentY, CurrentX - 1])
        {
            CurDirection = ChoiceRandom(1, 5, 1);
        }
        else if (CurrentY == 4 || TrailCheckArray[CurrentY + 1, CurrentX])
        {
            CurDirection = ChoiceRandom(1, 5, 4);
        }
        else if (CurrentX == 4 || TrailCheckArray[CurrentY, CurrentX + 1])
        {
            CurDirection = ChoiceRandom(1, 5, 3);
        }

        if (CurDirection == Direction)//전 방향과 같다면
        {
            goto RandomStart;//다시 탐색해
        }
        else
        {
            Direction = CurDirection;//저장
            Debug.Log("MyDirection is " + Direction);
            //isPlaceGood = true;
        }
    }

    
}
