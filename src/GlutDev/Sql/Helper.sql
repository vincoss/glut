SELECT * FROM GlutResultItem;

SELECT * FROM GlutProject;

INSERT INTO GlutProject
VALUES
('Gabo', '2019-09-04 07:47:59.0000000', '2019-09-04 07:47:59.0000000', 'Ferdinand Lukasak' )


SELECT * FROM GlutProject;
SELECT * FROM GlutRunAttribute;
SELECT * FROM GlutResultItem;

/*
    DELETE FROM GlutProject;
    DELETE FROM GlutRunAttribute;
    DELETE FROM GlutResultItem;
*/

SELECT strftime('%Y-%m-%d %H:%M:%S.0000000', EndDateTimeUtc), StatusCode, Count(*) AS Items FROM GlutResultItem
GROUP BY StatusCode, strftime('%Y-%m-%d %H:%M:%S.0000000', EndDateTimeUtc)
ORDER BY EndDateTimeUtc

SELECT strftime('%Y-%m-%d %H:%M:%S.0000000', EndDateTimeUtc), EndDateTimeUtc FROM GlutResultItem
ORDER BY EndDateTimeUtc;


SELECT * FROM GlutProject;
SELECT * FROM GlutRunAttribute;
SELECT * FROM GlutResultItem;

/*
    DELETE FROM GlutProject;
    DELETE FROM GlutRunAttribute;
    DELETE FROM GlutResultItem;
*/

SELECT strftime('%Y-%m-%d %H:%M:%S.0000000', EndDateTimeUtc), StatusCode, Count(*) AS Items FROM GlutResultItem
GROUP BY StatusCode, strftime('%Y-%m-%d %H:%M:%S.0000000', EndDateTimeUtc)
ORDER BY EndDateTimeUtc



SELECT strftime('%Y-%m-%d %H:%M:%S.0000000', '2019-09-06 02:20:53.7667527');

SELECT strftime('%Y-%m-%d %H:%M:%S.0000000', EndDateTimeUtc), EndDateTimeUtc FROM GlutResultItem
ORDER BY EndDateTimeUtc;

--2019-09-06 02:20:53.7667527