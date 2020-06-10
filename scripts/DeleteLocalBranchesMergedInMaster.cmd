:: Run this script in your git root directory
::
:: Running this script will remove all local branches that have
:: already been merged in the master branch

@echo off
:: Checkout the master branch
git checkout master
git pull
:: Foreach branch (except the current) that is merged into the current
FOR /F "eol=* delims=" %%b in ('git branch --merged') DO (	
	:: delete the branch
	git branch -d %%b
)
echo:
echo:The following commits are not yet merged to master

FOR /F "eol=* delims=" %%b in ('git branch') DO (		
	echo: 
	:: Log the commits that are on the branch, but not on master
	git log --pretty=oneline --abbrev-commit master..%%b
)

