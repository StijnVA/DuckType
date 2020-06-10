@echo off
for /f %%i in ('dir /s /a:d /b ^| findstr /i /e "/bin/"') do rd /s /q %%i
for /f %%i in ('dir /s /a:d /b ^| findstr /i /e "/obj/"') do rd /s /q %%i