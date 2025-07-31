import subprocess
import sys

def run_ncal():
    """
    ncal 셸 명령어를 실행하고 그 출력을 출력하는 함수입니다.
    """
    print("--- ncal 명령어 실행 시작 ---")
    try:
        # 'ncal' 명령어를 실행합니다.
        # capture_output=True: 명령어의 표준 출력과 표준 에러를 캡처합니다.
        # text=True: 캡처된 출력을 텍스트(문자열)로 디코딩합니다. (기본값은 바이트)
        # check=True: 명령어가 0이 아닌 종료 코드를 반환하면 CalledProcessError 예외를 발생시킵니다.
        result = subprocess.run(['ncal'], capture_output=True, text=True, check=True)

        # ncal 명령의 표준 출력 내용을 출력합니다.
        print(result.stdout)

        # 표준 에러가 있을 경우 출력합니다.
        if result.stderr:
            print("\n--- ncal 표준 에러 (있을 경우) ---")
            print(result.stderr)

        print(f"--- ncal 명령어 성공적으로 실행됨. 종료 코드: {result.returncode} ---")

    except FileNotFoundError:
        # 'ncal' 명령어를 찾을 수 없을 때 발생하는 오류를 처리합니다.
        print(f"오류: 'ncal' 명령어를 찾을 수 없습니다.")
        print("macOS/Linux 시스템에 'ncal'이 설치되어 있고 PATH에 있는지 확인하세요.")
        print("Windows에서는 이 명령어가 기본적으로 제공되지 않습니다.")
        sys.exit(1) # 스크립트 종료

    except subprocess.CalledProcessError as e:
        # 'ncal' 명령어가 실패했을 때 발생하는 오류를 처리합니다.
        print(f"오류: 'ncal' 명령어가 실패했습니다. 종료 코드: {e.returncode}")
        print("--- 표준 출력 ---")
        print(e.stdout)
        print("--- 표준 에러 ---")
        print(e.stderr)
        sys.exit(1) # 스크립트 종료

    except Exception as e:
        # 예상치 못한 다른 오류를 처리합니다.
        print(f"예기치 않은 오류 발생: {e}")
        sys.exit(1) # 스크립트 종료

if __name__ == "__main__":
    run_ncal()