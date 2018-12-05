import Data.Char
import Control.Parallel (par)

parallelMap :: (a -> b) -> [a] -> [b]
parallelMap f (x:xs) = let r = f x
                       in r `par` r : parallelMap f xs
parallelMap _ _      = []

letters = "qwertyuiopasdfghjklzxcvbnm"

main :: IO ()
main = do
  contents <- readFile "input"
  let contents' = parallelMap (\c -> enterStringMachine contents c) letters
  let contents'' = parallelMap length contents'
  let minimum = foldr min 50000 contents''
  putStrLn $ show $ minimum

  let result = enterStringMachine contents '0'
  putStrLn $ show $ length result

enterStringMachine :: String -> Char -> String
enterStringMachine string c =
  let string' = stringMachine string c
  in if length string' == length string
     then string'
     else enterStringMachine string' c

stringMachine :: String -> Char -> String
stringMachine [] _ = []
stringMachine [y] c = if y == c then [] else [y]
stringMachine (x:y:rest) c =
  if toLower x == c then
    stringMachine (y:rest) c
  else if reacts x y then
    stringMachine rest c
  else (x:) $ stringMachine (y:rest) c

reacts x y =
  differentCase x y && sameLetter x y

differentCase x y =
  (isLower x && isUpper y) ||
  (isLower y && isUpper x)

sameLetter x y =
  toLower x == toLower y

