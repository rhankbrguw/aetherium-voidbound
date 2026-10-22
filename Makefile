-include .env
export

UNITY ?= /home/samaele/Unity/Hub/Editor/6000.6.3f1/Editor/Unity
PROJECT_PATH := $(shell pwd)
LOG_FILE := /tmp/unity-build.log

.PHONY: help setup build-linux build-windows clean-cache test verify-constraints

help:
	@echo "Aetherium: Voidbound Build Targets"
	@echo "  make setup              - Initialize environment and check requirements"
	@echo "  make test               - Run Unity EditMode unit tests"
	@echo "  make build-linux        - Build standalone Linux x86_64 binary"
	@echo "  make build-windows      - Build standalone Windows x86_64 binary"
	@echo "  make clean-cache        - Clear Unity temporary files and cache"
	@echo "  make verify-constraints - Audit codebase for line limits and constraints"

setup:
	@which $(UNITY) > /dev/null 2>&1 || (echo "Unity binary not found at $(UNITY). Set UNITY=/path/to/Unity" && exit 1)
	@echo "Unity Editor found: $(UNITY)"

DOTNET := /home/samaele/Unity/Hub/Editor/6000.6.3f1/Editor/Data/DotNetSdk/dotnet

test:
	@echo "Running unit tests (NUnit via .NET SDK)..."
	@$(DOTNET) test Tests/Tests.csproj --verbosity normal

test-unity:
	@echo "Running Unity EditMode tests via batchmode..."
	@$(UNITY) -batchmode -runTests -testPlatform EditMode -projectPath "$(PROJECT_PATH)" -testResults /tmp/editmode-results.xml -logFile $(LOG_FILE) || (cat $(LOG_FILE) && exit 1)
	@echo "Tests completed. Results saved to /tmp/editmode-results.xml"

build-linux:
	@echo "Starting Linux standalone build..."
	@$(UNITY) -batchmode -quit -projectPath "$(PROJECT_PATH)" -executeMethod GameBuildPipeline.BuildLinux -logFile $(LOG_FILE) || (cat $(LOG_FILE) && exit 1)
	@echo "Linux build finished in Builds/Linux/"

build-windows:
	@echo "Starting Windows standalone build..."
	@$(UNITY) -batchmode -quit -projectPath "$(PROJECT_PATH)" -executeMethod GameBuildPipeline.BuildWindows -logFile $(LOG_FILE) || (cat $(LOG_FILE) && exit 1)
	@echo "Windows build finished in Builds/Windows/"

clean-cache:
	@echo "Cleaning cache and temporary build directories..."
	@rm -rf Library/ Temp/ Obj/ Logs/ Builds/

verify-constraints:
	@echo "Verifying code constraints (max 150 lines/file, zero TODOs)..."
	@find Assets/_Project -name "*.cs" -exec wc -l {} + | awk '$$2 != "total" && $$1 > 150 { print "VIOLATION: " $$2 " exceeds 150 lines (" $$1 ")" ; err=1 } END { if (err) exit 1 }'
	@! grep -rn "TODO" Assets/_Project/Scripts/ Assets/_Project/Tests/ || (echo "VIOLATION: TODO comments detected" && exit 1)
	@! grep -rn "NotImplementedException" Assets/_Project/Scripts/ Assets/_Project/Tests/ || (echo "VIOLATION: NotImplemented stubs detected" && exit 1)
	@echo "All constraints passed."
